namespace Chat_Warriors.GameLogic.player_management;

public enum WeaponType
{
    Sword,
    Dagger,
    Overweight
}

public class Helmet :Item{

    public Helmet ( string name, int price, int def, double defPerc) : base(name, price)
    {
        Def = def;
        DefPerc = defPerc;
    }
    public int Def { get; set; }
    public double DefPerc { get; set; }
}

public class Chestplate :Item{
    public Chestplate ( string name, int price, int def, double defPerc) : base(name, price)
    {
        Def = def;
        DefPerc = defPerc;
    }
    public int Def { get; set; }
    public double DefPerc { get; set; }
}

public class Leggins :Item{
    public Leggins ( string name, int price, int def, double defPerc) : base(name, price)
    {
        Def = def;
        DefPerc = defPerc;
    }
    public int Def { get; set; }
    public double DefPerc { get; set; }
}

public class Armour
{
    public Armour(Chestplate? chest = null, Leggins? legs = null, Helmet? helmet = null)
    {
        if (chest != null)
        {
            Chest = chest;    
        }

        if (legs != null)
        {
            Leggins = legs;
        }

        if (helmet != null)
        {
            Helm = helmet;
        }
    }

    public Chestplate Chest { get; set; }
    public Helmet Helm { get; set; }
    public Leggins Leggins { get; set; }

    public int TotalDefence()
    {
        int chestDef = 0;
        int helmDef = 0;
        int leggDef = 0;
        if (Chest != null)
        {
            chestDef = Chest.Def;
        }
        if (Leggins!= null)
        {
            leggDef = Leggins.Def;
        }if (Helm != null)
        {
            helmDef = Helm.Def;
        }
        return chestDef + leggDef + helmDef;
    }
}

public class Weapon: Item
{
    public Weapon ( string name, int price, WeaponType weaponType, int attack) : base(name, price){
        WeaponType = weaponType;
        Attack = attack;
    }
    public WeaponType WeaponType { get; set; }
    public int Attack { get; set; }
}

public class Equipment
{
    public Equipment()
    {
        EquipArmour();
        
    }

    public Armour equipped = new Armour();
    public void EquipArmour(Chestplate chest = null, Leggins legs = null, Helmet helmet = null)
    {
        equipped = new Armour(chest, legs, helmet);
    }
    
    public Weapon EquippedWeapon { get; set; }
    
}

//Player.Equipment.EquipedArmour.totaldefence