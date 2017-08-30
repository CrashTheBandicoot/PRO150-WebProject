
var canvas = document.getElementById("gameBoard");
var ctx = canvas.getContext('2d');
function makeBoard() {
    for (i = 0; i < 8; i++) {
        for (j = 0; j < 8; j++){
            if (((i == 0 || i == 2 || i == 4 || i == 6) && (j == 1 || j == 3 || j == 5 || j == 7)) || ((i == 1 || i == 3 || i == 5 || i == 7) && (j == 0 || j == 2 || j == 4 || j == 6))) {
                drawSquare('b', i, j);
            }
            else {
                drawSquare('r', i, j);
            }
        }
    }
}
makeBoard();

function drawSquare(color, x, y) {
    if (color == 'r') {
        ctx.fillStyle = "rgb(255, 0, 0)";
    }
    else {
        ctx.fillStyle = "rgb(0, 0, 0)";
    }
    ctx.fillRect(x*50, y*50, 50, 50);
}