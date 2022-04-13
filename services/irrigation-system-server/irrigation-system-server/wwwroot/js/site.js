// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const url = "https://localhost:7019"

function post(data, endpoint) {
    fetch(url + endpoint, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data)
    })
        .then(response => {
            console.log('Succcess: ', response)
        })
        .catch((error) => {
            console.error('Error:', error)
        })
}

function sendData() {
    post({message: "Test"}, "/home/sendData")
}

function callValve() {
    post({duration: 10, valveId: "123123", clientId: "ESP8266Client-testing"}, "/home/callValve")
}