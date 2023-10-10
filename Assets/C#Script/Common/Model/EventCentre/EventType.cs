namespace C_Script.Common.Model.EventCentre
{
    public enum CombatEventType
    {
        UpdateAllData,
        PlayerHurt,
        PlayerDeath,
        PlayerStop,
        PlayerStart,
        EnemyStart,
        EnemyStop,
        EnterBossRoom,
        CriticalStrike,
        EnemyNumChange,
        UseBloodBottle
    }

    public enum ScenesEventType
    {
        ReStart,
        GameOver,
        LevelPass,
        Loop,
        ClearRecord,
        OpenBlackBoard
    }
}