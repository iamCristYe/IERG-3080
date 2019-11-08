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
        public string imgsrc { get; protected set; }
        public Block()
        {
            IsWin = false;
            IsYou = false;
        }

        public class Text : Block
        {
            public Text()
            {
                IsPush = true;
                IsStop = true;
            }

            public class TextBaba : Text
            {
                public TextBaba()
                {
                    imgsrc = "/Resources/icons/BABA_TEXT.png";
                }
            }
            public class TextRock : Text
            {
                public TextRock()
                {
                    imgsrc = "/Resources/icons/ROCK_TEXT.png";
                }
            }
            public class TextFlag : Text
            {
                public TextFlag()
                {
                    imgsrc = "/Resources/icons/FLAG_TEXT.png";
                }
            }
            public class TextWall : Text
            {
                public TextWall()
                {
                    imgsrc = "/Resources/icons/WALL_TEXT.png";
                }
            }
            public class TextIs : Text
            {
                public TextIs()
                {
                    imgsrc = "/Resources/icons/IS_TEXT.png";
                }
            }
            public class TextYou : Text
            {
                public TextYou()
                {
                    imgsrc = "/Resources/icons/YOU_TEXT.png";
                }
            }
            public class TextWin : Text
            {
                public TextWin()
                {
                    imgsrc = "/Resources/icons/WIN_TEXT.png";
                }
            }
            public class TextPush : Text
            {
                public TextPush()
                {
                    imgsrc = "/Resources/icons/PUSH_TEXT.png";
                }
            }
            public class TextStop : Text
            {
                public TextStop()
                {
                    imgsrc = "/Resources/icons/STOP_TEXT.png";
                }
            }
        }

        public class Thing : Block
        {
            public Thing()
            {
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
        }
    }
}
