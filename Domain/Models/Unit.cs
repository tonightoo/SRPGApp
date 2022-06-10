using System;

namespace Domain.Models
{
    [Serializable]
    public class Unit
    {

        private string name;

        private int imageId;

        private int maxHp;

        private int currentHp;

        private int maxMp;

        private int currentMp;

        private int attack;

        private int deffence;

        private int magicAttack;

        private int magicDeffence;

        private int technic;

        private int luck;

        private int step;

        private int teamId;

        private Weapon weapon;

        private bool isAttacked = false;

        private bool isMoved = false;

        /// <summary>
        /// 名前
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        /// <summary>
        /// 画像ID
        /// </summary>
        public int ImageId
        {
            get { return imageId; }
            set { imageId = value; }
        }

        /// <summary>
        /// 最大HP
        /// </summary>
        public int MaxHp
        {
            get { return maxHp; }
            set {
                if (value <= 0) throw new ArgumentException();
                maxHp = value; 
            }
        }

        /// <summary>
        /// 現在HP
        /// </summary>
        public int CurrentHp
        {
            get { return currentHp; }
            set {
//                if (value < 0)
//                {
//                    currentHp = 0; 
//                    return;
//                }

                if (value > maxHp)
                {
                    currentHp = maxHp;
                    return;
                }
                    
                currentHp = value; 
            }
        }

        /// <summary>
        /// 最大MP
        /// </summary>
        public int MaxMp
        {
            get { return maxMp; }
            set {
                if (value < 0) throw new ArgumentException();
                maxMp = value; 
            }
        }

        /// <summary>
        /// 現在MP
        /// </summary>
        public int CurrentMp
        {
            get { return currentMp; }
            set {
                if (value < 0)
                {
                    value = 0;
                    return;
                }

                if (value > maxMp)
                {
                    currentMp = maxMp;
                    return;
                }
                currentMp = value; 
            }
        }

        /// <summary>
        /// 攻撃力
        /// </summary>
        public int Attack
        {
            get { return attack; }
            set {
                if (value <= 0) throw new ArgumentException();
                attack = value; 
            }
        }

        /// <summary>
        /// 防御力
        /// </summary>
        public int Deffence
        {
            get { return deffence; }
            set {
                if (value <= 0) throw new ArgumentException();
                deffence = value; 
            }
        }

        /// <summary>
        /// 魔法攻撃力
        /// </summary>
        public int MagicAttack
        {
            get { return magicAttack; }
            set {
                if (value <= 0) throw new ArgumentException();
                magicAttack = value; 
            }
        }

        /// <summary>
        /// 魔法防御力
        /// </summary>
        public int MagicDeffence
        {
            get { return magicDeffence; }
           set {
                if (value <= 0) throw new ArgumentException();
                magicDeffence = value;
           }
        }

        /// <summary>
        /// テクニック
        /// </summary>
        public int Technic
        {
            get { return technic; }
            set {
                if (value < 0) throw new ArgumentException();
                technic = value; 
            }
        }

        /// <summary>
        /// 運
        /// </summary>
        public int Luck
        {
            get { return luck; }
            set {
                if (value < 0) throw new ArgumentException();
                luck = value; 
            }
        } 

        /// <summary>
        /// 移動力
        /// </summary>
        public int Step
        {
            get { return step; }
            set {
                if (value <= 0) throw new ArgumentException();
                step = value; 
            }
        }

        public int TeamId
        {
            get { return teamId; }
            set { teamId = value; }
        }

        public Weapon Weapon
        {
            get { return weapon; }
            set { weapon = value; }
        }

        public bool IsAttacked
        {
            get { return isAttacked; }
            set { isAttacked = value; }
        }

        public bool IsMoved
        {
            get { return isMoved; }
            set { isMoved = value; }
        }


        private Unit()
        {
        }


        /// <summary>
        /// UnitのBuilder
        /// </summary>
        public class Builder
        {

           private Unit _unit;

            public Builder(Unit unit)
            {
                _unit = unit;
            }

            /// <summary>
            /// Unitのインスタンスを返す
            /// </summary>
            /// <returns></returns>
            public Unit Build()
            {
                return _unit;
            }

            /// <summary>
            /// 名前を設定して次に設定の必要な画像ID用のBuilderを返す
            /// </summary>
            /// <param name="name">ユニットの名前</param>
            /// <returns>画像ID用Builder</returns>
            public static ImageBuilder SetName(string name)
            {
                Unit unit = new Unit();
                unit.Name = name;
                return new ImageBuilder(unit);
            }

            /// <summary>
            /// 画像ID用のBuilder
            /// </summary>
            public class ImageBuilder
            {
                private Unit unit;

                internal ImageBuilder(Unit unit)
                {
                    this.unit = unit;
                }

                /// <summary>
                /// 画像IDを設定して、次に設定のプ必要な最大HPのBuilderを返す
                /// </summary>
                /// <param name="imageId">画像ID</param>
                /// <returns>最大HP用のBuilder</returns>
                public MaxHpBuilder SetImageId(int imageId)
                {
                    this.unit.ImageId = imageId;
                    return new MaxHpBuilder(unit);
                }

            }

            /// <summary>
            /// 最大HP用のBuilder
            /// </summary>
            public class MaxHpBuilder
            {
                private Unit unit;

                internal MaxHpBuilder(Unit unit)
                {
                    this.unit=unit;
                }

                /// <summary>
                /// 最大HPを設定して、現在HP用のBuilderを返す
                /// </summary>
                /// <param name="maxHp">最大HP</param>
                /// <returns>現在HP用のBuilder</returns>
                public CurrentHpBuilder SetMaxHp(int maxHp)
                {
                    this.unit.MaxHp = maxHp;
                    return new CurrentHpBuilder(unit);
                }

                /// <summary>
                /// 最大HP・現在HPを設定して最大MP用のBuilderを返す
                /// </summary>
                /// <param name="maxHp">最大HP</param>
                /// <returns>最大MP用のBuilder</returns>
                public MaxMpBuilder SetMaxHpAndHp(int maxHp)
                {
                    this.unit.MaxHp = maxHp;
                    this.unit.CurrentHp = maxHp;
                    return new MaxMpBuilder(unit);
                }
            }

            /// <summary>
            /// 現在HP用のBuilder
            /// </summary>
            public class CurrentHpBuilder
            {
                private Unit unit;

                internal CurrentHpBuilder(Unit unit)
                {
                    this.unit = unit;
                }

                /// <summary>
                /// 現在HPを設定して最大MP用のBuilderを返す
                /// </summary>
                /// <param name="currentHp">現在HP</param>
                /// <returns>最大MP用のBuilder</returns>
                public MaxMpBuilder SetCurrentHp(int currentHp)
                {
                    this.unit.CurrentHp = currentHp;
                    return new MaxMpBuilder(unit);
                }
            }

            /// <summary>
            /// 最大MP用のBuilder
            /// </summary>
            public class MaxMpBuilder
            {
                private Unit unit;

                internal MaxMpBuilder(Unit unit)
                {
                    this.unit=unit;
                }

                /// <summary>
                /// 最大MPを設定して次に設定の必要な現在MPのBuilderを返す
                /// </summary>
                /// <param name="maxMp">最大MP</param>
                /// <returns>現在MP用のBuilder</returns>
                public CurrentMpBuilder SetMaxMp(int maxMp)
                {
                    this.unit.MaxMp = maxMp;
                    return new CurrentMpBuilder(unit);
                }

                public AttackBuilder SetMaxMpAndMp(int maxMp)
                {
                    this.unit.MaxMp = maxMp;
                    this.unit.CurrentMp = maxMp;
                    return new AttackBuilder(unit);
                }

            }

            /// <summary>
            /// 現在MP用のBuilder
            /// </summary>
            public class CurrentMpBuilder
            {
                private Unit unit;

                internal CurrentMpBuilder(Unit unit)
                {
                    this.unit=unit;
                }

                /// <summary>
                /// 現在MPを設定して次に設定の必要な攻撃力用のBuilderを返す
                /// </summary>
                /// <param name="currentMp"></param>
                /// <returns></returns>
                public AttackBuilder SetCurrentMp(int currentMp)
                {
                    this.unit.CurrentMp=currentMp;
                    return new AttackBuilder(unit);
                }

            }

            /// <summary>
            /// 攻撃力用のBuilder
            /// </summary>
            public class AttackBuilder
            {
                private Unit unit;

                internal AttackBuilder(Unit unit)
                {
                    this.unit = unit;
                }

                /// <summary>
                /// 攻撃力を設定して次に設定の必要な防御力用のBuilderを返す
                /// </summary>
                /// <param name="attack">攻撃力</param>
                /// <returns>防御力用のBuilder</returns>
                public DeffenceBuilder SetAttack(int attack)
                {
                    this.unit.Attack = attack;
                    return new DeffenceBuilder(unit);
                }
            }

            /// <summary>
            /// 防御力用のBuilder
            /// </summary>
            public class DeffenceBuilder
            {
                private Unit unit;

                internal DeffenceBuilder(Unit unit)
                {
                    this.unit = unit;
                }

                /// <summary>
                /// 防御力を設定して魔法攻撃力用のBuilderを返す
                /// </summary>
                /// <param name="deffence">防御力</param>
                /// <returns>魔法攻撃力用のBuilder</returns>
                public MagicAttackBuilder SetDeffece(int deffence)
                {
                    this.unit.Deffence = deffence;
                    return new MagicAttackBuilder(unit);
                }
            }

            /// <summary>
            /// 魔法攻撃力用のBuilder
            /// </summary>
            public class MagicAttackBuilder
            {
                private Unit unit;

                internal MagicAttackBuilder(Unit unit)
                {
                    this.unit = unit;
                }
                
                /// <summary>
                /// 魔法攻撃力を設定して魔法防御力用のBuilderを返す
                /// </summary>
                /// <param name="magicAttack">魔法攻撃力</param>
                /// <returns>魔法防御力用のBuilder</returns>
                public MagicDeffenceBuilder SetMagicAttack(int magicAttack)
                {
                    this.unit.MagicAttack = magicAttack;
                    return new MagicDeffenceBuilder(unit);
                }
            }

            /// <summary>
            /// 魔法防御力用のBuilder
            /// </summary>
            public class MagicDeffenceBuilder
            {
                private Unit unit;

                internal MagicDeffenceBuilder(Unit unit)
                {
                    this.unit = unit;
                }

                /// <summary>
                /// 魔法防御力を設定してテクニック用のBuilderを返す
                /// </summary>
                /// <param name="magicDeffence">魔法防御力</param>
                /// <returns>テクニック用のBuilder</returns>
                public TechnicBuilder SetMagicDeffence(int magicDeffence)
                {
                    this.unit.MagicDeffence = magicDeffence;
                    return new TechnicBuilder(unit);
                }
            }

            /// <summary>
            /// テクニック用のBuilder
            /// </summary>
            public class TechnicBuilder
            {
                private Unit unit;

                internal TechnicBuilder(Unit unit)
                {
                    this.unit=unit;
                }

                /// <summary>
                /// テクニックを設定して運用のBuilderを返す
                /// </summary>
                /// <param name="technic">テクニック</param>
                /// <returns>運用のBuilder</returns>
                public LuckBuilder SetTechnic(int technic)
                {
                    this.unit.Technic = technic;
                    return new LuckBuilder(unit);
                }
            }

            /// <summary>
            /// 運用のBuilder
            /// </summary>
            public class LuckBuilder
            {
                private Unit unit;

                internal LuckBuilder(Unit unit)
                {
                    this.unit = unit;
                }

                /// <summary>
                /// 運を設定して移動力用のBuilderを返す
                /// </summary>
                /// <param name="luck">運</param>
                /// <returns>移動力用のBuilder</returns>
                public StepBuilder SetLuck(int luck)
                {
                    this.unit.luck = luck;
                    return new StepBuilder(unit);
                }
            }

            /// <summary>
            /// 移動力用のBuilder
            /// </summary>
            public class StepBuilder
            {
                private Unit unit;

                internal StepBuilder(Unit unit)
                {
                    this.unit = unit;
                }

                /// <summary>
                /// 移動力を設定してBuildコマンドを使えるBuilderを返す
                /// </summary>
                /// <param name="step">移動力</param>
                /// <returns>Builder</returns>
                public TeamIdBuilder SetStep(int step)
                {
                    this.unit.Step = step;
                    return new TeamIdBuilder(unit);
                }

            }

            public class TeamIdBuilder
            {
                private Unit unit;

                internal TeamIdBuilder(Unit unit)
                {
                    this.unit = unit;
                }

                public WeaponBuilder SetTeamId(int teamId)
                {
                    this.unit.TeamId = teamId;
                    return new WeaponBuilder(unit);
                }
            }

            public class WeaponBuilder
            {
                private Unit unit;

                internal WeaponBuilder(Unit unit)
                {
                    this.unit = unit;
                }

                public Builder SetWeapon(Weapon.WeaponType type)
                {
                    this.unit.Weapon = new Weapon(type);
                    return new Builder(unit);
                }


            }

        }


    }
}
