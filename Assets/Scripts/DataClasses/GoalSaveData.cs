namespace DataClasses
{
    public class GoalSaveData
    {
        public readonly int[] GoalIDs;
        public readonly int[] GoalAmounts;
        
        public GoalSaveData(int[] goalIDs, int[] goalAmounts)
        {
            GoalIDs = goalIDs;
            GoalAmounts = goalAmounts;
        }
    }
}