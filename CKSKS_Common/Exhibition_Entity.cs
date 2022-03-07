using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORS_RCM_Common
{
    public class Exhibition_Entity
    {
        private string item_code = string.Empty;
        public string Item_Code
        {
            get { return item_code; }
            set { item_code = value; }
        }

        private string item_name = string.Empty;
        public string Item_Name
        {
            get { return item_name; }
            set { item_name = value; }
        }

        private string catalogInfo = string.Empty;
        public string CatalogInfo
        {
            get { return catalogInfo; }
            set { catalogInfo = value; }
        }

        private string brandName = string.Empty;
        public string BrandName
        {
            get { return brandName; }
            set { brandName = value; }
        }

        private string companyName = string.Empty;
        public string CompanyName
        {
            get { return companyName; }
            set { companyName = value; }
        }

        private string competitionName = string.Empty;
        public string CompetitionName
        {
            get { return competitionName; }
            set { competitionName = value; }
        }

        private string className = string.Empty;
        public string ClassName
        {
            get { return className; }
            set { className = value; }
        }

        private string year = string.Empty;
        public string Year
        {
            get { return year; }
            set { year = value; }
        }

        private DateTime? startDate = null;
        public DateTime? StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        private DateTime? endDate = null;
        public DateTime? EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        private string season = string.Empty;
        public string Season
        {
            get { return season; }
            set { season = value; }
        }

        private int exhibitedUser = -1;
        public int ExhibitedUser
        {
            get { return exhibitedUser; }
            set { exhibitedUser = value; }
        }

        private string mall = string.Empty;
        public string Mall
        {
            get { return mall; }
            set { mall = value; }
        }

        private string remark = string.Empty;
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        private int errorCheck = -1;
        public int ErrorCheck
        {
            get { return errorCheck; }
            set { errorCheck = value; }
        }

        private int apiCheck = -1;
        public int ApiCheck
        {
            get { return apiCheck; }
            set { apiCheck = value; }
        }

        private int exhibitionCheck = -1;
        public int ExhibitionCheck
        {
            get { return exhibitionCheck; }
            set { exhibitionCheck = value; }
        }

        private string instructionno;
        public string Instructionno
        {
            get { return instructionno; }
            set { instructionno = value; }
        }
    }
}
