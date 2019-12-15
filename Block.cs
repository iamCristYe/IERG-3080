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
        public string Facing { get; set; }
        public bool Slipped { get; set; }   //to tell whether a block has slipped for a certain keydown

        public string imgsrc { get; protected set; }
        public string text { get; protected set; }
        public virtual void ReturnToDefault() { }
        public Block()
        {
            Facing = "right";
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
                Slipped = false;
            }
            public class TextBaba : ThingText
            {
                public TextBaba()
                {
                    imgsrc = "/Resources/icons/BABA_TEXT.png";
                    text = "BABA";
                }
            }
            public class TextRock : ThingText
            {
                public TextRock()
                {
                    imgsrc = "/Resources/icons/ROCK_TEXT.png";
                    text = "ROCK";
                }
            }
            public class TextFlag : ThingText
            {
                public TextFlag()
                {
                    imgsrc = "/Resources/icons/FLAG_TEXT.png";
                    text = "FLAG";
                }
            }
            public class TextWall : ThingText
            {
                public TextWall()
                {
                    imgsrc = "/Resources/icons/WALL_TEXT.png";
                    text = "WALL";
                }
            }

            public class TextBest : ThingText
            {
                public TextBest()
                {
                    imgsrc = "/Resources/icons/BEST_TEXT.png";
                    text = "BEST";
                }
            }

            public class TextGoop : ThingText
            {
                public TextGoop()
                {
                    imgsrc = "/Resources/icons/GOOP_TEXT.png";
                    text = "GOOP";
                }
            }
            public class TextLava : ThingText
            {
                public TextLava()
                {
                    imgsrc = "/Resources/icons/LAVA_TEXT.png";
                    text = "LAVA";
                }
            }
            public class TextBone : ThingText
            {
                public TextBone()
                {
                    imgsrc = "/Resources/icons/BONE_TEXT.png";
                    text = "BONE";
                }
            }
            public class TextGrass : ThingText
            {
                public TextGrass()
                {
                    imgsrc = "/Resources/icons/GRASS_TEXT.png";
                    text = "GRAS";
                }
            }
            public class TextKeke : ThingText
            {
                public TextKeke()
                {
                    imgsrc = "/Resources/icons/KEKE_TEXT.png";
                    text = "KEKE";
                }
            }

            public class TextIce : ThingText
            {
                public TextIce()
                {
                    imgsrc = "/Resources/icons/ICE_TEXT.png";
                    text = "ICE.";
                }
            }
            public class TextLove : ThingText
            {
                public TextLove()
                {
                    imgsrc = "/Resources/icons/LOVE_TEXT.png";
                    text = "LOVE";
                }
            }
            public class TextEmpty : ThingText
            {
                public TextEmpty()
                {
                    imgsrc = "/Resources/icons/EMPTY_TEXT.png";
                    text = "EMPT";
                }
            }

            public class TextAnni : ThingText
            {
                public TextAnni()
                {
                    imgsrc = "/Resources/icons/ANNI_TEXT.png";
                    text = "ANNI";
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
                Slipped = false;
            }

            public class TextIs : SpecialText
            {
                public TextIs()
                {
                    imgsrc = "/Resources/icons/IS_TEXT.png";
                    text = "I.S.";
                }
            }
            public class TextYou : SpecialText
            {
                public TextYou()
                {
                    imgsrc = "/Resources/icons/YOU_TEXT.png";
                    text = "YOU.";
                }
            }
            public class TextWin : SpecialText
            {
                public TextWin()
                {
                    imgsrc = "/Resources/icons/WIN_TEXT.png";
                    text = "WIN.";
                }
            }
            public class TextPush : SpecialText
            {
                public TextPush()
                {
                    imgsrc = "/Resources/icons/PUSH_TEXT.png";
                    text = "PUSH";
                }
            }
            public class TextStop : SpecialText
            {
                public TextStop()
                {
                    imgsrc = "/Resources/icons/STOP_TEXT.png";
                    text = "STOP";
                }
            }

            public class TextSink : SpecialText
            {
                public TextSink()
                {
                    imgsrc = "/Resources/icons/SINK_TEXT.png";
                    text = "SINK";
                }
            }

            public class TextSlip : SpecialText
            {
                public TextSlip()
                {
                    imgsrc = "/Resources/icons/SLIP_TEXT.png";
                    text = "SLIP";
                }
            }
            public class TextHot : SpecialText
            {
                public TextHot()
                {
                    imgsrc = "/Resources/icons/HOT_TEXT.png";
                    text = "HOT.";
                }
            }
            public class TextMove : SpecialText
            {
                public TextMove()
                {
                    imgsrc = "/Resources/icons/MOVE_TEXT.png";
                    text = "MOVE";
                }
            }
            public class TextMelt : SpecialText
            {
                public TextMelt()
                {
                    imgsrc = "/Resources/icons/MELT_TEXT.png";
                    text = "MELT";
                }
            }
            public class TextKill : SpecialText
            {
                public TextKill()
                {
                    imgsrc = "/Resources/icons/KILL_TEXT.png";
                    text = "KILL";
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
                Slipped = false;
            }
            public class Baba : Thing
            {
                public Baba()
                {
                    imgsrc = "/Resources/icons/BABA_THING.png";
                    text = "baba";
                }
            }
            public class Keke : Thing
            {
                public Keke()
                {
                    imgsrc = "/Resources/icons/KEKE_THING.png";
                    text = "keke";
                }
            }
            public class Rock : Thing
            {
                public Rock()
                {
                    imgsrc = "/Resources/icons/ROCK_THING.png";
                    text = "rock";
                }
            }
            public class Flag : Thing
            {
                public Flag()
                {
                    imgsrc = "/Resources/icons/FLAG_THING.png";
                    text = "flag";
                }
            }
            public class Wall : Thing
            {
                public Wall()
                {
                    imgsrc = "/Resources/icons/WALL_THING.png";
                    text = "OOOO";
                }
            }

            public class Border : Thing
            {
                public Border()
                {
                    imgsrc = "/Resources/icons/BORDER_THING.png";
                    text = "bord";
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
                    Slipped = false;
                }
            }

            public class FakeWall : Thing
            {
                public FakeWall()
                {
                    imgsrc = "/Resources/icons/FAKEWALL_THING.png";
                    text = "oooo";
                }
            }
            public class Grass : Thing
            {
                public Grass()
                {
                    imgsrc = "/Resources/icons/GRASS_THING.png";
                    text = "grass";
                }
            }
            public class Goop : Thing
            {
                public Goop()
                {
                    imgsrc = "/Resources/icons/GOOP_THING.png";
                    text = "goop";
                }
            }
            public class Lava : Thing
            {
                public Lava()
                {
                    imgsrc = "/Resources/icons/LAVA_THING.png";
                    text = "lava";
                }
            }
            public class Ice : Thing
            {
                public Ice()
                {
                    imgsrc = "/Resources/icons/ICE_THING.png";
                    text = "ice.";
                }
            }
            public class Love : Thing
            {
                public Love()
                {
                    imgsrc = "/Resources/icons/LOVE_THING.png";
                    text = "love";
                }
            }
            public class Bone : Thing
            {
                public Bone()
                {
                    imgsrc = "/Resources/icons/BONE_THING.png";
                    text = "bone";
                }
            }

            public class Anni : Thing
            {
                public Anni()
                {
                    imgsrc = "/Resources/icons/ANNI_THING.png";
                    text = "anni";
                }
            }
            public class Empty : Thing
            {
                public Empty()
                {
                    imgsrc = "";
                    text = "";
                }
            }
        }
    }
}
