//
// Created by mattl on 30/04/2022.
//

#include "security.h"

#define sizeofarray(a) (sizeof(a) / sizeof(a[0]))
#define ONE_HOUR_IN_SECS 3600
#define NTP_SERVERS "pool.ntp.org", "time.nist.gov"

static const char* device_key = CONFIG_DEVICE_KEY;
static uint8_t signature[512];
static unsigned char encrypted_signature[32];
static char base64_decoded_device_key[32];


static uint32_t getSecondsSinceEpoch()
{
    return (uint32_t)time(nullptr);
}


int generateSasToken(az_iot_hub_client &client, char* sas_token, size_t size)
{
    az_span signature_span = az_span_create((uint8_t*)signature, sizeofarray(signature));
    az_span out_signature_span;
    az_span encrypted_signature_span
            = az_span_create((uint8_t*)encrypted_signature, sizeofarray(encrypted_signature));

    uint32_t expiration = getSecondsSinceEpoch() + ONE_HOUR_IN_SECS;

    // Get signature
    if (az_result_failed(az_iot_hub_client_sas_get_signature(
            &client, expiration, signature_span, &out_signature_span)))
    {
        Serial.println("Failed getting SAS signature");
        return 1;
    }

    // Base64-decode device key
    int base64_decoded_device_key_length
            = base64_decode_chars(device_key, strlen(device_key), base64_decoded_device_key);

    if (base64_decoded_device_key_length == 0)
    {
        Serial.println("Failed base64 decoding device key");
        return 1;
    }

    // SHA-256 encrypt
    br_hmac_key_context kc;
    br_hmac_key_init(
            &kc, &br_sha256_vtable, base64_decoded_device_key, base64_decoded_device_key_length);

    br_hmac_context hmac_ctx;
    br_hmac_init(&hmac_ctx, &kc, 32);
    br_hmac_update(&hmac_ctx, az_span_ptr(out_signature_span), az_span_size(out_signature_span));
    br_hmac_out(&hmac_ctx, encrypted_signature);

    // Base64 encode encrypted signature
    String b64enc_hmacsha256_signature = base64::encode(encrypted_signature, br_hmac_size(&hmac_ctx));

    az_span b64enc_hmacsha256_signature_span = az_span_create(
            (uint8_t*)b64enc_hmacsha256_signature.c_str(), b64enc_hmacsha256_signature.length());

    // URl-encode base64 encoded encrypted signature
    if (az_result_failed(az_iot_hub_client_sas_get_password(
            &client,
            expiration,
            b64enc_hmacsha256_signature_span,
            AZ_SPAN_EMPTY,
            sas_token,
            size,
            nullptr)))
    {
        Serial.println("Failed getting SAS token");
        return 1;
    }

    return 0;
}