using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabaIsYou.Model
{
    class Block
    {
        public bool IsPush { get; protected set; }
        public bool IsStop { get; protected set; }
        public bool IsYou { get; protected set; }
        public bool IsWin { get; protected set; }
        public bool IsSink { get; protected set; }
        public bool IsKill { get; protected set; }
        public bool IsMove { get; protected set; }
        //slip hot melt
        public string imgsrc { get; protected set; }
        public virtual void ReturnToDefault() { }
        public Block()
        {
            ReturnToDefault();
        }

        public class ThingText : Block
        {
            public override void ReturnToDefault()
            {
                IsWin = false;
                IsYou = false;
                IsSink = false;
                IsKill = false;
                IsMove = false;
                IsPush = true;
                IsStop = true;
            }
            public class TextBaba : ThingText
            {
                public TextBaba()
                {
                    imgsrc = "/Resources/icons/BABA_TEXT.png";
                }
            }
            public class TextRock : ThingText
            {
                public TextRock()
                {
                    imgsrc = "/Resources/icons/ROCK_TEXT.png";
                }
            }
            public class TextFlag : ThingText
            {
                public TextFlag()
                {
                    imgsrc = "/Resources/icons/FLAG_TEXT.png";
                }
            }
            public class TextWall : ThingText
            {
                public TextWall()
                {
                    imgsrc = "/Resources/icons/WALL_TEXT.png";
                }
            }
        }

        public class SpecialText : Block
        {
            public override void ReturnToDefault()
            {
                IsWin = false;
                IsYou = false;
                IsSink = false;
                IsKill = false;
                IsMove = false;
                IsPush = true;
                IsStop = true;
            }

            public class TextIs : SpecialText
            {
                public TextIs()
                {
                    imgsrc = "/Resources/icons/IS_TEXT.png";
                }
            }
            public class TextYou : SpecialText
            {
                public TextYou()
                {
                    imgsrc = "/Resources/icons/YOU_TEXT.png";
                }
            }
            public class TextWin : SpecialText
            {
                public TextWin()
                {
                    imgsrc = "/Resources/icons/WIN_TEXT.png";
                }
            }
            public class TextPush : SpecialText
            {
                public TextPush()
                {
                    imgsrc = "/Resources/icons/PUSH_TEXT.png";
                }
            }
            public class TextStop : SpecialText
            {
                public TextStop()
                {
                    imgsrc = "/Resources/icons/STOP_TEXT.png";
                }
            }

        }

        public class Thing : Block
        {
            public override void ReturnToDefault()
            {
                IsWin = false;
                IsYou = false;
                IsSink = false;
                IsKill = false;
                IsMove = false;
                IsPush = false;
                IsStop = false;
            }
            public class Baba : Thing
            {
                public Baba()
                {
                    imgsrc = "/Resources/icons/BABA_THING.png";
                }
            }
            public class Rock : Thing
            {
                public Rock()
                {
                    imgsrc = "/Resources/icons/ROCK_THING.png";
                }
            }
            public class Flag : Thing
            {
                public Flag()
                {
                    imgsrc = "/Resources/icons/FLAG_THING.png";
                }
            }
            public class Wall : Thing
            {
                public Wall()
                {
                    imgsrc = "/Resources/icons/WALL_THING.png";
                }
            }

            public class Grass : Thing
            {
                public Grass()
                {
                    imgsrc = "/Resources/icons/GRASS_THING.png";
                }
                public override void ReturnToDefault()
                {
                    IsWin = false;
                    IsYou = false;
                    IsSink = false;
                    IsKill = false;
                    IsMove = false;
                    IsPush = false;
                    IsStop = true;
                }
            }

            public class FakeWall : Thing
            {
                public FakeWall()
                {
                    imgsrc = "/Resources/icons/FAKEWALL_THING.png";
                }
            }
        }
    }
}
