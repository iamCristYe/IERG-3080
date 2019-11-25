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
        public bool IsHot { get; protected set; }
        public bool IsMelt { get; protected set; }
        public bool IsSlip { get; protected set; }

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
                IsHot = false;
                IsMelt = false;
                IsSlip = false;
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

            public class TextBest : ThingText
            {
                public TextBest()
                {
                    imgsrc = "/Resources/icons/BEST_TEXT.png";
                }
            }

            public class TextGoop : ThingText
            {
                public TextGoop()
                {
                    imgsrc = "/Resources/icons/GOOP_TEXT.png";
                }
            }
            public class TextLava : ThingText
            {
                public TextLava()
                {
                    imgsrc = "/Resources/icons/LAVA_TEXT.png";
                }
            }
            public class TextBone : ThingText
            {
                public TextBone()
                {
                    imgsrc = "/Resources/icons/BONE_TEXT.png";
                }
            }
            public class TextGrass : ThingText
            {
                public TextGrass()
                {
                    imgsrc = "/Resources/icons/GRASS_TEXT.png";
                }
            }
            public class TextKeke : ThingText
            {
                public TextKeke()
                {
                    imgsrc = "/Resources/icons/KEKE_TEXT.png";
                }
            }

            public class TextIce : ThingText
            {
                public TextIce()
                {
                    imgsrc = "/Resources/icons/ICE_TEXT.png";
                }
            }
            public class TextLove : ThingText
            {
                public TextLove()
                {
                    imgsrc = "/Resources/icons/LOVE_TEXT.png";
                }
            }
            public class TextEmpty : ThingText
            {
                public TextEmpty()
                {
                    imgsrc = "/Resources/icons/EMPTY_TEXT.png";
                }
            }

            public class TextAnni : ThingText
            {
                public TextAnni()
                {
                    imgsrc = "/Resources/icons/ANNI_TEXT.png";
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
                IsHot = false;
                IsMelt = false;
                IsSlip = false;
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

            public class TextSink : SpecialText
            {
                public TextSink()
                {
                    imgsrc = "/Resources/icons/SINK_TEXT.png";
                }
            }

            public class TextSlip : SpecialText
            {
                public TextSlip()
                {
                    imgsrc = "/Resources/icons/SLIP_TEXT.png";
                }
            }
            public class TextHot : SpecialText
            {
                public TextHot()
                {
                    imgsrc = "/Resources/icons/HOT_TEXT.png";
                }
            }
            public class TextMove : SpecialText
            {
                public TextMove()
                {
                    imgsrc = "/Resources/icons/MOVE_TEXT.png";
                }
            }
            public class TextMelt : SpecialText
            {
                public TextMelt()
                {
                    imgsrc = "/Resources/icons/MELT_TEXT.png";
                }
            }
            public class TextKill : SpecialText
            {
                public TextKill()
                {
                    imgsrc = "/Resources/icons/KILL_TEXT.png";
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
                IsHot = false;
                IsMelt = false;
                IsSlip = false;
            }
            public class Baba : Thing
            {
                public Baba()
                {
                    imgsrc = "/Resources/icons/BABA_THING.png";
                }
            }
            public class Keke : Thing
            {
                public Keke()
                {
                    imgsrc = "/Resources/icons/KEKE_THING.png";
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

            public class Border : Thing
            {
                public Border()
                {
                    imgsrc = "/Resources/icons/BORDER_THING.png";
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
                    IsHot = false;
                    IsMelt = false;
                    IsSlip = false;
                }
            }

            public class FakeWall : Thing
            {
                public FakeWall()
                {
                    imgsrc = "/Resources/icons/FAKEWALL_THING.png";
                }
            }
            public class Grass : Thing
            {
                public Grass()
                {
                    imgsrc = "/Resources/icons/GRASS_THING.png";
                }
            }
            public class Goop : Thing
            {
                public Goop()
                {
                    imgsrc = "/Resources/icons/GOOP_THING.png";
                }
            }
            public class Lava : Thing
            {
                public Lava()
                {
                    imgsrc = "/Resources/icons/LAVA_THING.png";
                }
            }
            public class Ice : Thing
            {
                public Ice()
                {
                    imgsrc = "/Resources/icons/ICE_THING.png";
                }
            }
            public class Love : Thing
            {
                public Love()
                {
                    imgsrc = "/Resources/icons/LOVE_THING.png";
                }
            }
            public class Bone : Thing
            {
                public Bone()
                {
                    imgsrc = "/Resources/icons/BONE_THING.png";
                }
            }

            public class Anni : Thing
            {
                public Anni()
                {
                    imgsrc = "/Resources/icons/ANNI_THING.png";
                }
            }
        }
    }
}
