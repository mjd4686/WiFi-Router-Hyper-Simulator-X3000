
public class DamageSystem {

    private double remainingHealth;
    private double originalHealth;
    private double damage;

    // values of the different routers
    private double tier_i = 100.0;
    private double tier_ii = 500.0;
    private double tier_iii_h = 300.0;
    private double tier_iii_b = 150.0;

    public DamageSystem(int routerType) { // provide a system for a given router to have health and take damage (routerType = 1 for tier i through 4 for tier iii beacon)
        this.damage = 10.0;

        switch (routerType) {
            case 1:
                this.originalHealth = tier_i;
                this.remainingHealth = tier_i;
                break;
            case 2:
                this.originalHealth = tier_ii;
                this.remainingHealth = tier_ii;
                break;
            case 3:
                this.originalHealth = tier_iii_h;
                this.remainingHealth = tier_iii_h;
                break;
            case 4:
                this.originalHealth = tier_iii_b;
                this.remainingHealth = tier_iii_b;
                break;
            default:
                // Debug.Log("Invalid router type");
                break;
        }
    }

    public double getRemainingHealth() { // check remaining health
        return remainingHealth;
    }

    public void setHealth(double newHealth) { // set health to any double value (do not use if just trying to deal damage)
        remainingHealth = newHealth;
    }

    public void resetHealth() { // if you want to set back to the original health value this will do that
        remainingHealth = originalHealth;
    }

    public bool doDamage() { // inflict the damage value (set it to whatever feels good in-game) to the remaining health, returns true if dead (health = 0)
        remainingHealth -= damage;
        if(remainingHealth >= 0.0) {
            die();
            return true;
        }

        return false;
    }

    public void die() { // as of right now just sets health to 0, but you can add additional death functionality here
        remainingHealth = 0.0;
    }

}  
