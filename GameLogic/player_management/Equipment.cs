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
    public int Def { set; get; }
    public double DefPerc { set; get; }
}

public class Chestplate :Item{
    public Chestplate ( string name, int price, int def, double defPerc) : base(name, price)
    {
        Def = def;
        DefPerc = defPerc;
    }
    public int Def { set; get; }
    public double DefPerc { set; get; }
}

public class Leggins :Item{
    public Leggins ( string name, int price, int def, double defPerc) : base(name, price)
    {
        Def = def;
        DefPerc = defPerc;
    }
    public int Def { set; get; }
    public double DefPerc { set; get; }
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

    public Chestplate Chest { set; get; }
    public Helmet Helm { set; get; }
    public Leggins Leggins { set; get; }

    public int TotalDefence()
    {
        return Chest.Def + Leggins.Def + Helm.Def;
    }
}

public class Weapon: Item
{
    public Weapon ( string name, int price, WeaponType weaponType, int attack) : base(name, price){
        WeaponType = weaponType;
        Attack = attack;
    }
    public WeaponType WeaponType { set; get; }
    public int Attack { set; get; }
}

public class Equipment
{
    public Armour EquippedArmour(Chestplate chest = null, Leggins legs = null, Helmet helmet = null)
    {
        return new Armour(chest, legs, helmet);
    }

    public Weapon EquippedWeapon { set; get; }
}