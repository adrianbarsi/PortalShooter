using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System.Numerics;

public class Room {
    static readonly int ROOM_WIDTH = Game.WINDOW_WIDTH - (Game.WINDOW_PADDING * 2);
    static readonly int ROOM_HEIGHT = Game.WINDOW_HEIGHT - (Game.WINDOW_PADDING * 2);
    public static readonly int ROOM_START_X = Game.WINDOW_PADDING;
    public static readonly int ROOM_START_Y = Game.WINDOW_PADDING;
    public static readonly int ROOM_END_X = ROOM_START_X + ROOM_WIDTH;
    public static readonly int ROOM_END_Y = ROOM_START_Y + ROOM_HEIGHT;
    static readonly int ROOM_DIMENSION = 10;
    static readonly int ROOM_X_STEP = ROOM_WIDTH / ROOM_DIMENSION;
    static readonly int ROOM_Y_STEP = ROOM_HEIGHT / ROOM_DIMENSION;
    static readonly int SEQUENCE_LENGTH = 9;

    List<Cell> cells = new();
    Portal[] portals = new Portal[SEQUENCE_LENGTH];
    Player player;
    
    public Room(Player player) {
        this.player = player;
        this.player.Position = new Vector2(
            ROOM_START_X + ROOM_X_STEP * (ROOM_DIMENSION / 2),
            ROOM_START_Y + ROOM_Y_STEP * (ROOM_DIMENSION - 1) + (ROOM_Y_STEP / 2)
        );
        
        int[] shuffledCellIndexes = new int[ROOM_DIMENSION * ROOM_DIMENSION];

        for (int i = 0; i < ROOM_DIMENSION * ROOM_DIMENSION; i++) {
            shuffledCellIndexes[i] = i;
        }

        Random random = new Random();

        random.Shuffle(shuffledCellIndexes);

        int[] portalIndexes = new int[SEQUENCE_LENGTH];
        int portalIndexesCount = 0;

        int[] invalidPortalIndexes = [
            ROOM_DIMENSION * (ROOM_DIMENSION / 2) - 1,
            ROOM_DIMENSION * (ROOM_DIMENSION / 2) - 2,
            ROOM_DIMENSION * (ROOM_DIMENSION / 2 + 1) - 1,
            ROOM_DIMENSION * (ROOM_DIMENSION / 2 + 1) - 2,
        ];

        for (int i = 0; i < shuffledCellIndexes.Length && portalIndexesCount < SEQUENCE_LENGTH; i++) {
            int shuffledCellIndex = shuffledCellIndexes[i];
        
            if (!invalidPortalIndexes.Any(index => index == shuffledCellIndex)) {
                portalIndexes[portalIndexesCount] = shuffledCellIndex;
                portalIndexesCount++;
            }
        }

        for (int x = 0; x < ROOM_DIMENSION; x++) {
            for (int y = 0; y < ROOM_DIMENSION; y++) {
                int index = x * ROOM_DIMENSION + y;

                int startingXPosition = ROOM_X_STEP * x + ROOM_START_X;
                int startingYPosition = ROOM_Y_STEP * y + ROOM_START_Y;

                Vector2 position = new(
                    startingXPosition + (ROOM_X_STEP / 2),
                    startingYPosition + (ROOM_Y_STEP / 2)
                );

                Rectangle bounds = new(
                    startingXPosition,
                    startingYPosition,
                    ROOM_X_STEP,
                    ROOM_Y_STEP
                );

                int foundPortalOrderIndex = Array.FindIndex(portalIndexes, portalIndex => portalIndex == index);

                if (foundPortalOrderIndex != -1) {
                    int foundPortalOrder = foundPortalOrderIndex + 1;
                    string orderDisplay = foundPortalOrder.ToString();

                    Portal portal = new(
                        position: position,
                        bounds: bounds,
                        order: foundPortalOrder,
                        orderDisplay: orderDisplay,
                        topSide: new Line(
                            new Vector2(
                                position.X - (ROOM_X_STEP / 2),
                                position.Y - (ROOM_Y_STEP / 2)
                            ),
                            new Vector2(
                                position.X + (ROOM_X_STEP / 2),
                                position.Y - (ROOM_Y_STEP / 2)
                            )
                        ),
                        rightSide: new Line(
                            new Vector2(0, 0),
                            new Vector2(0, 0)
                        ), 
                        bottomSide: new Line(
                            new Vector2(0, 0),
                            new Vector2(0, 0)
                        ),
                        leftSide: new Line(
                            new Vector2(0, 0),
                            new Vector2(0, 0)
                        )
                    );

                    cells.Add(portal);
                    portals[foundPortalOrderIndex] = portal;
                }
                else {
                    cells.Add(new Cell(position, bounds));
                }
            }
        }
    }

    public void update() {
        player.update();
    }

    public void draw() {
        DrawRectangleLines(ROOM_START_X, ROOM_START_Y, ROOM_WIDTH, ROOM_HEIGHT, WHITE);
        
        foreach (Portal portal in portals) {
            portal.draw();
        }

        player.draw();
    }
}
