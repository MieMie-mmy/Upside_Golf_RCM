using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ORS_RCM_Common
{
  public  class PromotionExhibition_Entity
    {
      private int id;
      private string itemcode;
      private string mall;
      private string shop;
      private int ctrl;
      private int shopid;
      private int api;
      private int error;
      private int exhibitior;
      private int backcheck;
      private string itemname;
      private string catlogInfo;
      private string brandname;
      private string companyname;
      private string comptname;
      private string classname;
      private string year;
      private string season;
      private string remark;
      private DateTime? exdate1=null;
      private DateTime? exdate2= null;


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
      public string Year
      {
          get { return year; }
          set { year = value; }
      }
      public string Mall
      {
          get { return mall; }
          set { mall = value; }
      }
      public string Season
      {
          get { return season; }
          set { season = value; }
      }
      public string Shop 
      {
          get { return shop; }
          set { shop = value; }
      }
      public int Ctrl
      {
          get { return ctrl; }
          set { ctrl = value; }
      }
      public int ShopID
      {
          get { return shopid; }
          set { shopid = value; }
      }
      public int API
      {
          get { return api; }
          set { api = value; }
      }
      public int Error
      {
          get { return error; }
          set { error = value; }
      }
      public int Exhibitior
      {
          get { return exhibitior; }
          set { exhibitior = value; }
      }
      public int Backcheck
      {
          get { return backcheck; }
          set { backcheck = value; }
      }
      public string CatlogInfo
      {
          get { return catlogInfo; }
          set { catlogInfo = value; }
      }
      public string Brandname
      {
          get { return brandname; }
          set { brandname = value; }
      }
      public string Companyname
      {
          get { return companyname; }
          set { companyname = value; }
      }
      public string Competitionname
      {
          get { return comptname; }
          set { comptname = value; }
      }
      public string Classname
      {
          get { return classname; }
          set { classname = value; }
      }
      public string Remark
      {
          get { return remark; }
          set { remark = value; }
      }
      public DateTime? Ehbxdate1
      {
          get { return exdate1; }
          set { exdate1 = value; }
      }
      public DateTime? Ehbxdate2
      {
          get { return exdate2; }
          set { exdate2 = value; }
      }
    }
}
