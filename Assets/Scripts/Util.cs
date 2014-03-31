using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Util
{
    public static float GetAngleFromDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.LEFT:
                return -90;
            case Direction.RIGHT:
                return 90;
            case Direction.DOWN:
                return 180;
            case Direction.UP:
                return 0;
        }

        return 0;
    }
}
