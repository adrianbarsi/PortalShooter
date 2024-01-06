using static Raylib_cs.Raylib;

Game game = new Game();

while (!WindowShouldClose()) {
  game.update();
  game.draw();
};

game.cleanup();