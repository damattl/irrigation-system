window.buildChart = (id, config) => {
    let ctx = document.getElementById(id).getContext('2d')
    new Chart(ctx, config)
}