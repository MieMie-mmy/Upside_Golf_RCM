using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORS_RCM_Common
{
   public  class Item_Information
    {
private string item_name = string.Empty;
private string salecode= string.Empty;
private string brand_name = string.Empty;
private string item_code = string.Empty;
private int  rate=0;
private int selling_price=0;
private int cost=0;
private int shop_status=0;
private string class_name = string.Empty;
private string season = string.Empty;
private string jan_code = string.Empty;
private int year=0;
private string competition_name = string.Empty;

public string Item_Code
{
    get { return item_code; }
    set { item_code = value; }
}


public string Item_Name
{
    get { return item_name; }
    set { item_name = value; }
}

public string Sale_Code
{
    get { return salecode; }
    set { salecode= value; }
}


public string Brand_Name
{
    get { return brand_name; }
    set { brand_name = value; }
}

public int List_Price
{
    get { return rate; }
    set { rate= value; }
}

public int Sale_Price
{
    get { return selling_price; }
    set { selling_price= value; }
}


public int  Cost
{
    get { return cost; }
    set { cost = value; }
}

public int Status
{
    get { return shop_status; }
    set { shop_status = value; }
}

public string Class_Name
{
    get { return class_name; }
    set { class_name = value; }
}

public string Season
{
    get { return season; }
    set { season= value; }
}



public string JAN_Code
{
    get { return jan_code; }
    set { jan_code = value; }
}

public int Year
{
    get { return year; }
    set { year = value; }
}

public string Competition_Name
{
    get { return competition_name; }
    set {competition_name= value; }
}






    }
}
