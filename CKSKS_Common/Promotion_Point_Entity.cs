using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ORS_RCM_Common
{
   public  class Promotion_Point_Entity
    {
       private int id;
       private string itemcode=null;
       private string itemname = null;
       private string brandname = null;
       private string year = null;
       private string season = null;
       private string competitionname = null;
       private string instructionnumber = null;
       private string shopname = null;
       private string claffication = null;

       public int ID
       {
           get { return id; }
           set { id = value; }
       }

       public string Itemcode 
       {
           get { return itemcode; }
           set { itemcode = value; }
       }

       public string Itemname 
       {
           get { return itemname; }
           set { itemname = value; }
       }

       public string Brandname 
       {
           get { return brandname; }
           set { brandname = value; }
       }

       public string Year 
       {
           get { return year; }
           set { year = value; }
       }

       public string Season 
       {
           get { return season; }
           set { season = value; }
       }

       public string Competationname 
       {
           get { return competitionname; }
           set { competitionname = value; }
       }

       public string InstructionNo 
       {
           get { return instructionnumber; }
           set { instructionnumber = value; }
       }

       public string Shopnmae 
       {
           get { return shopname; }
           set { shopname = value; }
       }

       public string Claffication 
       {
           get { return claffication; }
           set { claffication = value; }
       }

       private DateTime? rperiod = null;
       public DateTime? Rperiodto
       {
           get { return rperiod; }
           set { rperiod = value; }
       }

       private DateTime? rperiodfrom = null;
       public DateTime? Rperiodfrom
       {
           get { return rperiodfrom; }
           set { rperiodfrom = value; }
       }
    
       private DateTime? yperiodfrom = null;
       public DateTime? Yperiodfrom
       {
           get { return yperiodfrom; }
           set { yperiodfrom = value; }
       }

  

     

       private DateTime? yperiodto = null;
       public DateTime? Yperiodto
       {
           get { return yperiodto; }
           set { yperiodto = value; }
       }

       private DateTime?pperiodfrom = null;
       public DateTime? Pperiodfrom 
       {
           get { return pperiodfrom; }
           set { pperiodfrom = value; }
       }
       private DateTime? pperiodto = null;
       public DateTime? Pperiodto
       {
           get { return pperiodto; }
           set { pperiodto = value; }
       }

       private string shopstatus;
       public string Shopstatus 
       {
           get { return shopstatus; }
           set { shopstatus = value; }
       }

       private int Rp;
       public int RP
       {
           get{ return Rp; }
           set { Rp = value; }
       }
       private int Yp;
       public int YP
       {
           get { return Yp; }
           set { Yp = value; }
       }
       private int Pp;
       public int PP
       {
           get { return Pp; }
           set { Pp = value; }
       }
        #region for save
       private DateTime? rpperiod = null;
       public DateTime? RPointperiodto
       {
           get { return rpperiod; }
           set { rpperiod = value; }
       }



       private DateTime? rpperiodfrom = null;
       public DateTime? RPointperiodfrom
       {
           get { return rpperiodfrom; }
           set { rpperiodfrom = value; }
       }

    
       private DateTime? ypperiodfrom = null;
       public DateTime? YPointperiodfrom
       {
           get { return ypperiodfrom; }
           set { ypperiodfrom = value; }
       }
       private DateTime? ypperiodto = null;
       public DateTime? YPointperiodto
       {
           get { return ypperiodto; }
           set { ypperiodto = value; }
       }

       private DateTime? ppperiodfrom = null;
       public DateTime? PPointperiodfrom
       {
           get { return ppperiodfrom; }
           set { ppperiodfrom = value; }
       }
       private DateTime? ppperiodto = null;
       public DateTime? PPointperiodto
       {
           get { return ppperiodto; }
           set { ppperiodto = value; }
       }

       private int  Rpointmg;
       public int Rpimg 
       {
           get { return Rpointmg; }
           set { Rpointmg = value; }
       }
       private int ypointmg;
       public int Ypimg
       {
           get { return ypointmg; }
           set { ypointmg = value; }
       }
       private int ppointmg;
       public int Ppimg
       {
           get { return ppointmg; }
           set { ppointmg = value; }
       }

       private string rstart = null;
       public string Rstart 
       {
           get { return rstart; }
           set { rstart = value; }
       }

       private string ystart = null;
       public string Ystart 
       {
           get { return ystart; }
           set { ystart = value; }
       }

       private string pstart = null;
       public string Pstart 
       {
           get { return pstart; }
           set { pstart = value; }
       }

       private string rend = null;
       public string Rend 
       {
           get { return rend; }
           set { rend = value; }
       }
       private string yend = null;
       public string Yend
       {
           get { return yend; }
           set { yend = value; }
       }
       private string pend = null;
       public string Pend
       {
           get { return pend; }
           set { pend = value; }
       }
        #endregion
    }
}
