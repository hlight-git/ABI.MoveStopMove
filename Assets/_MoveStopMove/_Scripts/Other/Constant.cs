using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Constant
{
    public class Anim
    {
        public class Ranger
        {
            public const string IDLE = "Idle";
            public const string ATTACK = "Attack";
            public const string DIE = "Die";
            public const string WIN = "Win";
            public const string RUN = "Run";
            public const string DANCE = "Dance";

            public const float IDLE_DURATION = 4.4f;
            public const float ATTACK_DURATION = 1.1f;
            public const float DIE_DURATION = 2.2f;
            public const float WIN_DURATION = 15.567f;
            public const float RUN_DURATION = 0.633f;
            public const float DANCE_DURATION = 15.667f;

            public const float THROW_DELAY_TIME = 0.4f;
        }
    }
    public class Ranger
    {
        public const float MIN_SIZE = 0.5f;
        public const float DEFAULT_SIZE = 1f;
        public const float MAX_SIZE = 2.5f;

        public const float MIN_ATTACK_SPEED = 0.5f;
        public const float DEFAULT_ATTACK_SPEED = 1f;
        public const float MAX_ATTACK_SPEED = 2f;

        public const float DEFAULT_ATTACK_RANGE = 5f;

        public const float GROWTH_PER_SCORE = 0.05f;
        public const float BOT_REVIVE_TIME = 3f;

        public const int COIN_GAIN_PER_SCORE = 5;
    }
    public class Weapon
    {
        public const float RELOAD_TIME = 0.5f;
        public const float BULLET_LIFE_TIME_AFTER_HIT_OBSTACLE = 1f;
        public const float BULLET_PUSH_FORCE = 200f;
        public const float DEFAULT_BULLET_BASE_SPEED = 10f;
        public const float DEFAULT_BULLET_ROTATION_SPEED = 10f;
    }
    public class Tag
    {
        public const string CHARACTER = "Character";
        public const string BULLET = "Bullet";
        public const string OBSTACLE = "Obstacle";
    }

    public class Collection
    {
        public const int FIRST_ELEM_INDEX = 0;
    }

    public class Level
    {
        public const int DEFAULT_ACTIVE_BOT_AMOUNT = 10;
        public const int DEFAULT_CHARACTER_AMOUNT = 50;
    }

    public class Indicator
    {
        public static readonly Vector2 HORIZONTAL_VISIBLE_AREA_BOUND = new Vector2(0.075f, 0.925f);
        public static readonly Vector2 VERTICAL_VISIBLE_AREA_BOUND = new Vector2(0.05f, 0.95f);
    }
    public class Prototype
    {
        public static readonly List<UnitColor> UNIT_COLORS = System.Enum.GetValues(typeof(UnitColor)).Cast<UnitColor>().ToList();
        public static readonly string[] BOT_NAMES =
        {
            "Victor",
            "Kenneth",
            "Parker",
            "Bobby",
            "James",
            "Walter",
            "Clark",
            "Cooper",
            "Perez",
            "Alexander",
            "Rogers",
            "Morris",
            "Campbell",
            "Garcia",
            "Carol",
            "Lawrence",
            "Hughes",
            "Beverly",
            "Anderson",
            "Daniel",
            "Adams",
            "Catherine",
            "Marie",
            "Bennett",
            "Carolyn",
            "Patterson",
            "Harris",
            "Bonnie",
            "Johnny",
            "Moore",
            "Marilyn",
            "Chris",
            "Powell",
            "Gonzales",
            "Wilson",
            "Lopez",
            "Murphy",
            "Gregory",
            "Martinez",
            "Carter",
            "Coleman",
            "Mitchell",
            "Carlos ",
            "Edwards",
            "Kathleen",
            "Howard",
            "Christine",
            "Torres",
            "Jose",
            "Christina",
            "Davis",
            "Virginia",
            "Paula",
            "Louise",
            "Amanda",
            "Judy",
            "Philip",
            "Coleman",
            "Campbell",
            "Hernandez",
            "Mitchell",
            "Lori",
            "Kathy",
            "Diane",
            "Emily",
            "Shirley",
            "Miller",
            "Rogers",
            "Powell",
            "Rivera",
            "Bonnie",
            "Craig",
            "Michelle",
            "Laura",
            "Flores",
            "Brooks",
            "Richardson",
            "Walker",
        };
    }
}

public enum WeaponType
{
    W_Hammer_1 = PoolType.W_Hammer_1,
    W_Hammer_2 = PoolType.W_Hammer_2,
    W_Hammer_3 = PoolType.W_Hammer_3,
    W_Candy_1 = PoolType.W_Candy_1,
    W_Candy_2 = PoolType.W_Candy_2,
    W_Candy_3 = PoolType.W_Candy_3,
    W_Boomerang_1 = PoolType.W_Boomerang_1,
    W_Boomerang_2 = PoolType.W_Boomerang_2,
    W_Boomerang_3 = PoolType.W_Boomerang_3,
}

public enum BulletType
{
    B_Hammer_1 = PoolType.B_Hammer_1,
    B_Hammer_2 = PoolType.B_Hammer_2,
    B_Hammer_3 = PoolType.B_Hammer_3,
    B_Candy_1 = PoolType.B_Candy_1,
    B_Candy_2 = PoolType.B_Candy_2,
    B_Candy_3 = PoolType.B_Candy_3,
    B_Boomerang_1 = PoolType.B_Boomerang_1,
    B_Boomerang_2 = PoolType.B_Boomerang_2,
    B_Boomerang_3 = PoolType.B_Boomerang_3,
}

public enum HatType
{
    HAT_None = 0,
    HAT_Arrow = PoolType.HAT_Arrow,
    HAT_Cap = PoolType.HAT_Cap,
    HAT_Cowboy = PoolType.HAT_Cowboy,
    HAT_Crown = PoolType.HAT_Crown,
    HAT_Ear = PoolType.HAT_Ear,
    HAT_StrawHat = PoolType.HAT_StrawHat,
    HAT_Headphone = PoolType.HAT_Headphone,
    HAT_Horn = PoolType.HAT_Horn,
    HAT_Police = PoolType.HAT_Police,
}

public enum SkinType
{
    SKIN_Normal = PoolType.SKIN_Normal,
    SKIN_Devil = PoolType.SKIN_Devil,
    SKIN_Angle = PoolType.SKIN_Angle,
    SKIN_Witch = PoolType.SKIN_Witch,
    SKIN_Deadpool = PoolType.SKIN_Deadpool,
    SKIN_Thor = PoolType.SKIN_Thor,
}

public enum AccessoryType
{
    ACC_None = 0,
    ACC_Book = PoolType.ACC_Book,
    ACC_CaptainShield = PoolType.ACC_Captain,
    ACC_Headphone = PoolType.ACC_Headphone,
    ACC_Shield = PoolType.ACC_Shield,
}