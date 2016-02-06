
[System.Serializable]
public class Stats {
    public float playerHealth = 100f;
    public float playerRegenRate = 1f;
    public float playerRegenDelay = 1f;
    public float playerDivinity = 10f;
    public float playerDivinityRate = 1f;
    public float playerWalkSpeed = 20f;
    public float playerSprintSpeed = 35f;

    public Stats(float health, float regenRate, float regenDelay, float divinity, float divinityRate, float walkSpeed, float sprintSpeed)
    {
        playerHealth = health;
        playerRegenRate = regenRate;
        playerRegenDelay = regenDelay;
        playerDivinity = divinity;
        playerDivinityRate = divinityRate;
        playerWalkSpeed = walkSpeed;
        playerSprintSpeed = sprintSpeed;
  
    }

    public Stats()
    {

    }

}
