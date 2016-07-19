using Microsoft.Xna.Framework;

namespace AthameRPG.GameEngine
{
    public static class Animation
    {
        public static AnimationReturnedValue SpriteSheetAnimation(Vector2 lastAbstractCoord, Vector2 abstractPlayerPositon,string direction, int cropFrame, int cropWidth, int cropHeight, int cropStay, int north, int south, int east, int west, int northEast, int northWest, int southEast, int southWest)
        {
            Rectangle CropCurrentFramePlayer = new Rectangle();
            
            if (lastAbstractCoord.X != abstractPlayerPositon.X && lastAbstractCoord.Y != abstractPlayerPositon.Y)
            {
                if (lastAbstractCoord.X < abstractPlayerPositon.X && lastAbstractCoord.Y < abstractPlayerPositon.Y)
                {
                    CropCurrentFramePlayer = new Rectangle(cropFrame * cropWidth, northWest, cropWidth, cropHeight);
                    direction = "NW";
                }
                else if (lastAbstractCoord.X < abstractPlayerPositon.X &&
                         lastAbstractCoord.Y > abstractPlayerPositon.Y)
                {
                    CropCurrentFramePlayer = new Rectangle(cropFrame * cropWidth, southWest, cropWidth, cropHeight);
                    direction = "SW";
                }
                else if (lastAbstractCoord.X > abstractPlayerPositon.X &&
                         lastAbstractCoord.Y > abstractPlayerPositon.Y)
                {
                    CropCurrentFramePlayer = new Rectangle(10 + cropFrame * cropWidth, southEast, cropWidth, cropHeight);
                    direction = "SE";
                }
                else if (lastAbstractCoord.X > abstractPlayerPositon.X &&
                         lastAbstractCoord.Y < abstractPlayerPositon.Y)
                {
                    CropCurrentFramePlayer = new Rectangle(cropFrame * cropWidth, northEast, cropWidth, cropHeight);
                    direction = "NE";
                }
            }
            else if (lastAbstractCoord.X != abstractPlayerPositon.X)
            {
                if (lastAbstractCoord.X > abstractPlayerPositon.X)
                {
                    CropCurrentFramePlayer = new Rectangle(15 + cropFrame * cropWidth, east, cropWidth, cropHeight);
                    direction = "E";
                }
                else
                {
                    CropCurrentFramePlayer = new Rectangle(15 + cropFrame * cropWidth, west, cropWidth, cropHeight);
                    direction = "W";
                }
            }
            else if (lastAbstractCoord.Y != abstractPlayerPositon.Y)
            {
                if (lastAbstractCoord.Y > abstractPlayerPositon.Y)
                {
                    CropCurrentFramePlayer = new Rectangle(cropFrame * cropWidth, south, cropWidth, cropHeight);
                    direction = "S";
                }
                else
                {
                    CropCurrentFramePlayer = new Rectangle(cropFrame * cropWidth, north, cropWidth, cropHeight);
                    direction = "N";
                }
            }
            else
            {
                switch (direction)
                {

                    case "N":
                        CropCurrentFramePlayer = new Rectangle(cropStay, north, cropWidth, cropHeight);
                        break;
                    case "S":
                        CropCurrentFramePlayer = new Rectangle(cropStay, south, cropWidth, cropHeight);
                        break;
                    case "E":
                        CropCurrentFramePlayer = new Rectangle(cropStay, east, cropWidth, cropHeight);
                        break;
                    case "W":
                        CropCurrentFramePlayer = new Rectangle(cropStay, west, cropWidth, cropHeight);
                        break;
                    case "NE":
                        CropCurrentFramePlayer = new Rectangle(cropStay, northEast, cropWidth, cropHeight);
                        break;
                    case "SE":
                        CropCurrentFramePlayer = new Rectangle(cropStay, southEast, cropWidth, cropHeight);
                        break;
                    case "NW":
                        CropCurrentFramePlayer = new Rectangle(cropStay, northWest, cropWidth, cropHeight);
                        break;
                    case "SW":
                        CropCurrentFramePlayer = new Rectangle(cropStay, southWest, cropWidth, cropHeight);
                        break;

                }

            }

            return new AnimationReturnedValue(direction, CropCurrentFramePlayer);
        }

        public static AnimationReturnedValue BattlefieldAnimation(Vector2 lastDrawCoord, Vector2 warUnitDrawCoord,Rectangle[] cropStay, Rectangle[] cropMove, Rectangle[] cropAttack, Rectangle[] cropHit, int cropFrame, bool AmIAttacking)
        {
            Rectangle currentFrame = new Rectangle();

            if (AmIAttacking)
            {
                currentFrame = cropAttack[cropFrame];
            }
            else if (lastDrawCoord != warUnitDrawCoord)
            {

                currentFrame = cropMove[cropFrame];
            }
            else
            {
                currentFrame = cropStay[cropFrame];
            }


            return new AnimationReturnedValue(" ", currentFrame);
        }

        //public static AnimationReturnedValue BattlefieldAnimation(Vector2 lastDrawCoord, Vector2 warUnitDrawCoord, int cropStayRow, int cropMovingRow, int cropAttackRow, int cropFrame, int stayWidth, int stayHeight,int movingWidth, int movingHeight, int attackWidth, int attackHeight, int correctionStay, int correctionMove, int correctionAttack)
        //{
        //    Rectangle currentFrame = new Rectangle();

        //    if (lastDrawCoord.X != warUnitDrawCoord.X)
        //    {

        //        if (lastDrawCoord.X > warUnitDrawCoord.X)
        //        {
        //            currentFrame = new Rectangle(correctionMove + cropFrame * movingWidth, cropMovingRow, movingWidth, movingHeight);

        //        }
        //        else
        //        {

        //            currentFrame = new Rectangle(correctionMove + cropFrame * movingWidth, cropMovingRow, movingWidth, movingHeight);
        //        }
        //    }
        //    else
        //    {
        //        currentFrame = new Rectangle(correctionStay + cropFrame * stayWidth, cropStayRow, stayWidth, stayHeight);
        //    }


        //    return new AnimationReturnedValue(" ", currentFrame);
        //}
    }
}
