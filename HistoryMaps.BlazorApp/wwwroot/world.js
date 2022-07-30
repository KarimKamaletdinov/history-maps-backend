async function run() {
    console.log("run world");
    const canvas = document.querySelector('#world-canvas');
    const img = document.querySelector('#world-image');
    const context = canvas.getContext('2d');
    context.drawImage(img, 0, 0, canvas.clientWidth, canvas.clientHeight);
}