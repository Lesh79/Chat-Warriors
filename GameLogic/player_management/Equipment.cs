namespace Chat_Warriors.GameLogic.player_management;

public enum WeaponoryType
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
    public double DefPerc{ get; set; }
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
            Leggs = legs;
        }

        if (helmet != null)
        {
            Helm = helmet;
        }
    }

    public Chestplate Chest { get; set; }
    public Helmet Helm { get; set; }
    public Leggins Leggs { get; set; }

    public int TotalDefence()
    {
        int chestDef = (Chest != null) ? Chest.Def : 0 ;
        int helmDef = (Leggs != null) ? Leggs.Def : 0;
        int leggDef = (Helm != null) ? Helm.Def : 0;
        
        return chestDef + leggDef + helmDef;
    }
}

public class Weapon: Item
{
    
    public Weapon ( string name, int price, WeaponoryType weaponType, int attack) : base(name, price){
        WeaponType = weaponType;
        Attack = attack;
    }
    public WeaponoryType WeaponType { get; set; }
    public int Attack { get; set; }
}

public class Equipment
{
    public Equipment()
    {
        EquipArmour();
        EquipWeapon();
    }

    public Armour PlayerArmour = new Armour();
    public Weapon PlayerWeapon;
    public void EquipArmour(Chestplate? chest = null, Leggins? legs = null, Helmet? helmet = null)
    {
        PlayerArmour = new Armour(chest, legs, helmet);
    }

    public void EquipWeapon(Weapon? weapon = null)
    {
        PlayerWeapon = weapon;
    }
}
