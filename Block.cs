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
                    imgsrc = "/Resources/icons/BABA_TEXT.jpg";
                }
            }
            public class TextRock : ThingText
            {
                public TextRock()
                {
                    imgsrc = "/Resources/icons/ROCK_TEXT.jpg";
                }
            }
            public class TextFlag : ThingText
            {
                public TextFlag()
                {
                    imgsrc = "/Resources/icons/FLAG_TEXT.jpg";
                }
            }
            public class TextWall : ThingText
            {
                public TextWall()
                {
                    imgsrc = "/Resources/icons/WALL_TEXT.jpg";
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
                    imgsrc = "/Resources/icons/IS_TEXT.jpg";
                }
            }
            public class TextYou : SpecialText
            {
                public TextYou()
                {
                    imgsrc = "/Resources/icons/YOU_TEXT.jpg";
                }
            }
            public class TextWin : SpecialText
            {
                public TextWin()
                {
                    imgsrc = "/Resources/icons/WIN_TEXT.jpg";
                }
            }
            public class TextPush : SpecialText
            {
                public TextPush()
                {
                    imgsrc = "/Resources/icons/PUSH_TEXT.jpg";
                }
            }
            public class TextStop : SpecialText
            {
                public TextStop()
                {
                    imgsrc = "/Resources/icons/STOP_TEXT.jpg";
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
                    imgsrc = "/Resources/icons/BABA_THING.jpg";
                }
            }
            public class Rock : Thing
            {
                public Rock()
                {
                    imgsrc = "/Resources/icons/ROCK_THING.jpg";
                }
            }
            public class Flag : Thing
            {
                public Flag()
                {
                    imgsrc = "/Resources/icons/FLAG_THING.jpg";
                }
            }
            public class Wall : Thing
            {
                public Wall()
                {
                    imgsrc = "/Resources/icons/WALL_THING.jpg";
                }
            }
        }
    }
}
