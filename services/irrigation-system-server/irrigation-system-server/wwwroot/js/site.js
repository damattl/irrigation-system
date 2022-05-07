let profile_chart = null;

window.buildChart = (id, config) => {
    if (profile_chart != null) {
        profile_chart.destroy()
        profile_chart = null
    }
    let ctx = document.getElementById(id).getContext('2d')
    profile_chart = new Chart(ctx, config)
}