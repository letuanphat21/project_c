document.addEventListener("DOMContentLoaded", function () {
    function createSnowflake() {
        const snowflake = document.createElement("div");
        snowflake.className = "snowflake";
        snowflake.innerHTML = "❄️";

        const randomX = Math.random() * 100;
        snowflake.style.setProperty('--randomX', `${randomX}%`);

        document.body.appendChild(snowflake);

        // Remove snowflake after animation ends
        setTimeout(() => {
            snowflake.remove();
        }, 15000); // 15 seconds
    }

    function checkSeason() {
        const now = new Date();
        const month = now.getMonth(); // Month is 0-indexed (0 = January, 11 = December)

        if (month >= 9 && month <= 11) { // Tháng 10 đến tháng 12 (10, 11, 12)
            setInterval(createSnowflake, 100); // Create a new snowflake every 100ms
        }
    }

    checkSeason();
});
