using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConstant
{
    public class Character
    {
        public const float ATTACK_ANIM_DURATION = 1.1f;
        public const float THROW_DELAY_TIME = 0.4f;

        public const float MIN_SIZE = 0.5f;
        public const float DEFAULT_SIZE = 1f;
        public const float MAX_SIZE = 4f;

        public const float MIN_ATTACK_SPEED = 0.5f;
        public const float DEFAULT_ATTACK_SPEED = 1f;
        public const float MAX_ATTACK_SPEED = 2f;

        public const float DEFAULT_ATTACK_RANGE = 5f;

        public const float SIZE_UP_PER_SCORE = 0.25f;
        public const float BOT_REVIVE_TIME = 3f;
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
        public const string BULLET_PATH = "BulletPath";
    }
    public class Layer
    {
        public const string PREDICTION = "Prediction";
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

    public class Percent
    {
        public const float _50 = 0.5f;
        public const float _100 = 1;
    }

    public class Indicator
    {
        public static readonly Vector2 HORIZONTAL_VIEWPORT_BOUND = new Vector2(0.075f, 0.925f);
        public static readonly Vector2 VERTICAL_VIEWPORT_BOUND = new Vector2(0.05f, 0.95f);
    }
}