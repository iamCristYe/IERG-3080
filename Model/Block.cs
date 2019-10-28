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
                    imgsrc = "";
                }
            }
            public class TextRock : Text
            {
                public TextRock()
                {
                    imgsrc = "";
                }
            }
            public class TextFlag : Text
            {
                public TextFlag()
                {
                    imgsrc = "";
                }
            }
            public class TextWall : Text
            {
                public TextWall()
                {
                    imgsrc = "";
                }
            }
            public class TextIs : Text
            {
                public TextIs()
                {
                    imgsrc = "";
                }
            }
            public class TextYou : Text
            {
                public TextYou()
                {
                    imgsrc = "";
                }
            }
            public class TextWin : Text
            {
                public TextWin()
                {
                    imgsrc = "";
                }
            }
            public class TextPush : Text
            {
                public TextPush()
                {
                    imgsrc = "";
                }
            }
            public class TextStop : Text
            {
                public TextStop()
                {
                    imgsrc = "";
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
                    imgsrc = "";
                }
            }
            public class Rock : Thing
            {
                public Rock()
                {
                    imgsrc = "";
                }
            }
            public class Flag : Thing
            {
                public Flag()
                {
                    imgsrc = "";
                }
            }
            public class Wall : Thing
            {
                public Wall()
                {
                    imgsrc = "";
                }
            }
        }
    }
}
