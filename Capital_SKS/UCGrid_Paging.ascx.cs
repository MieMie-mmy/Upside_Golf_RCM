using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace ORS_RCM
{
    public partial class UCGrid_Paging : System.Web.UI.UserControl
    {
        //to return index
        public delegate void SendMessageToThePageHandler(int PageIndex);

        public event SendMessageToThePageHandler sendIndexToThePage;

        private int totalRecord = 0;
        public int TotalRecord
        {
            get { return totalRecord; }
            set
            {
                totalRecord = value;
                hfTotalRecord.Value = totalRecord.ToString();
            }
        }

        private int onePageRecord = 0;
        public int OnePageRecord
        {
            get { return onePageRecord; }
            set
            {
                onePageRecord = value;
                hfOnePageRecord.Value = onePageRecord.ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        //protected void HidePageNo(LinkButton lnk)
        //{
        //    lnk.Visible = true;
        //    int currentIndex = Convert.ToInt32(lnk.Text);
        //    if (TotalRecord <= currentIndex * OnePageRecord)
        //    {
        //        if (OnePageRecord <= (currentIndex * OnePageRecord-TotalRecord))
        //        {
        //            lnk.Visible = false;
        //        }
        //    }
        //}

        //public void populatePaging()
        //{
        //    int currpgindex = Convert.ToInt32(hfCurrentPageNo.Value) / 5;

        //    if (Convert.ToInt32(hfCurrentPageNo.Value) % 5 == 0 && Convert.ToInt32(hfCurrentPageNo.Value)!=0)
        //        currpgindex--;

        //    if (currpgindex == 0)
        //        lnkPrevMore.Visible = false;
        //    else lnkPrevMore.Visible = true;


        //    int CurrentPageNo = Convert.ToInt32(hfCurrentPageNo.Value);

        //    if (CurrentPageNo == 1)
        //    {
        //        lnkPrev.Enabled = false; ;
        //    }
        //    else
        //    {
        //        lnkPrev.Enabled = true;
        //    }

        //    lnk1.Text = (currpgindex * 5 + 1).ToString();
        //    lnk2.Text = (currpgindex * 5 + 2).ToString();
        //    lnk3.Text = (currpgindex * 5 + 3).ToString();
        //    lnk4.Text = (currpgindex * 5 + 4).ToString();
        //    lnk5.Text = (currpgindex * 5 + 5).ToString();

        //    HidePageNo(lnk1);
        //    HidePageNo(lnk2);
        //    HidePageNo(lnk3);
        //    HidePageNo(lnk4);
        //    HidePageNo(lnk5);

        //    int LatestPageNo = totalRecord / OnePageRecord;
        //    int remainder = totalRecord % OnePageRecord;
        //    if (remainder > 0 && remainder < OnePageRecord)
        //        LatestPageNo++;

        //    hideNextMore(LatestPageNo);

        //    if (LatestPageNo == CurrentPageNo)
        //        lnkNext.Enabled = false;
        //    else lnkNext.Enabled = true;

        //    selectedPageNoColor();
        //}

        //protected void selectedPageNoColor()
        //{
        //    int currentPageNo = Convert.ToInt32(hfCurrentPageNo.Value)%5;
        //    if (currentPageNo == 0)
        //        currentPageNo = 5;

        //    for (int i = 1; i <= 5; i++)
        //    {
        //        LinkButton lnk = this.FindControl("lnk" + i) as LinkButton;
        //        if (i == (currentPageNo))
        //            lnk.BackColor = Color.Gainsboro;
        //        else lnk.BackColor = Color.White;
        //    }
        //}

        //protected void hideNextMore(int latestPage)
        //{
        //    lnkNextMore.Visible=true;
        //    if ((lnk1.Visible == true && lnk1.Text.Equals(latestPage.ToString())) ||
        //        (lnk2.Visible == true && lnk2.Text.Equals(latestPage.ToString())) ||
        //        (lnk3.Visible == true && lnk3.Text.Equals(latestPage.ToString())) ||
        //        (lnk4.Visible == true && lnk4.Text.Equals(latestPage.ToString())) ||
        //        (lnk5.Visible == true && lnk5.Text.Equals(latestPage.ToString()))
        //       )
        //    {
        //        lnkNextMore.Visible = false;
        //    }
        //}

        //protected void Paging_Click(object sender,EventArgs e)
        //{
        //    LinkButton lnk = sender as LinkButton;
        //    hfCurrentPageNo.Value = lnk.Text;
        //    populatePaging();
        //    lblCurrent.Text = hfCurrentPageNo.Value;
        //    lblShowPage.Text = lblCurrent.Text + "/" + lblTotal.Text;
        //    sendIndexToThePage(Convert.ToInt32(hfCurrentPageNo.Value)-1);
        //}

        //protected void lnkNext_Click(object sender, EventArgs e)
        //{
        //    int currentPageNo = Convert.ToInt32(hfCurrentPageNo.Value);
        //    currentPageNo++;
        //    hfCurrentPageNo.Value = currentPageNo.ToString();
        //    populatePaging();
        //    lblCurrent.Text = hfCurrentPageNo.Value;
        //    lblShowPage.Text = lblCurrent.Text + "/" + lblTotal.Text;
        //    sendIndexToThePage(currentPageNo - 1);
        //}

        //protected void lnkPrev_Click(object sender, EventArgs e)
        //{
        //    int currentPageNo = Convert.ToInt32(hfCurrentPageNo.Value);
        //    currentPageNo--;
        //    hfCurrentPageNo.Value = currentPageNo.ToString();
        //    populatePaging();
        //    lblCurrent.Text = hfCurrentPageNo.Value;
        //    lblShowPage.Text = lblCurrent.Text + "/" + lblTotal.Text;
        //    sendIndexToThePage(currentPageNo-1);
        //}

        //protected void lnkNextMore_Click(object sender, EventArgs e)
        //{
        //    int index = Convert.ToInt32(lnk5.Text);
        //    index++;
        //    hfCurrentPageNo.Value = index.ToString();
        //    populatePaging();
        //    lblCurrent.Text = hfCurrentPageNo.Value;
        //    lblShowPage.Text = lblCurrent.Text + "/" + lblTotal.Text;
        //    sendIndexToThePage(index - 1);
        //}

        //protected void lnkPrevMore_Click(object sender, EventArgs e)
        //{
        //    int index = Convert.ToInt32(lnk1.Text);
        //    index--;
        //    hfCurrentPageNo.Value = index.ToString();
        //    populatePaging();
        //    lblCurrent.Text = hfCurrentPageNo.Value;
        //    lblShowPage.Text = lblCurrent.Text + "/" + lblTotal.Text;
        //    sendIndexToThePage(index - 1);
        //}

        //protected void lnkFirst_Click(object sender, EventArgs e)
        //{
        //    hfCurrentPageNo.Value = "1";
        //    populatePaging();
        //    lblCurrent.Text = hfCurrentPageNo.Value;
        //    lblShowPage.Text = lblCurrent.Text + "/" + lblTotal.Text;
        //    sendIndexToThePage(0);
        //}

        //protected void lnkLast_Click(object sender, EventArgs e)
        //{
        //    int LatestPageNo = totalRecord / OnePageRecord;
        //    int remainder = totalRecord % OnePageRecord;
        //    if (remainder > 0 && remainder < OnePageRecord)
        //        LatestPageNo++;

        //    hfCurrentPageNo.Value = LatestPageNo.ToString();
        //    populatePaging();
        //    lblCurrent.Text = hfCurrentPageNo.Value;
        //    lblShowPage.Text = lblCurrent.Text + "/" + lblTotal.Text;
        //    sendIndexToThePage(LatestPageNo - 1);
        //}

        public void CalculatePaging(int totalRecord, int PageSize, int curIndex)
        {
            if (totalRecord <= 0)
            {
                this.Visible = false;
                return;
            }
            else this.Visible = true;

            lblTotalRecord.Text=totalRecord.ToString();

            lnkPagingNo1.Text = "1";
            lnkPagingNo2.Text = "2";
            lnkPagingNo3.Text = "3";
            lnkPagingNo4.Text = "4";
            lnkPagingNo5.Text = "5";

            lnkPagingNo1.Visible = false;
            lnkPagingNo2.Visible = false;
            lnkPagingNo3.Visible = false;
            lnkPagingNo4.Visible = false;
            lnkPagingNo5.Visible = false;
            lnkPagingNextMore.Visible = false;
            LinkButton lnkLast = new LinkButton();

            if ((totalRecord / PageSize) > 0 )
            {
                lnkPagingNo1.Visible = true;
                lnkLast = lnkPagingNo2;
            }
            if ((totalRecord / PageSize) > 1)
            {
                lnkPagingNo2.Visible = true;
                lnkLast = lnkPagingNo3;
            }
            if ((totalRecord / PageSize) > 2)
            {
                lnkPagingNo3.Visible = true;
                lnkLast = lnkPagingNo4;
            }
            if ((totalRecord / PageSize) > 3)
            {
                lnkPagingNo4.Visible = true;
                lnkLast = lnkPagingNo5;
            }
            if ((totalRecord / PageSize) > 4)
            {
                lnkPagingNo5.Visible = true;
                lnkPagingNextMore.Visible = true;
            }

            if (totalRecord % PageSize != 0)
                lnkLast.Visible = true;

            if (totalRecord > 0)
            {
                lnkPagingNo1.Visible = true;
            }
            else
            {
                lnkPagingNo1.Visible = false;
            }
                
            if (Convert.ToInt32(lnkPagingNo5.Text) <= 5)
            {
                lnkPagingPrevMore.Visible = false;
            }
            else lnkPagingPrevMore.Visible = true;

            int LatestPageNo = totalRecord / PageSize;
            int remainder = totalRecord % PageSize;
            if (remainder < PageSize && remainder!=0)
                LatestPageNo++;

            if (LatestPageNo == 0 && totalRecord > 0)
                LatestPageNo++;

            lblTotal.Text = LatestPageNo.ToString();
            lblCurrent.Text = curIndex.ToString();
            int start = ((curIndex - 1) * PageSize)+1;
            int end = curIndex * PageSize;

            //lblShowPage.Text = lblCurrent.Text + "/" + lblTotal.Text;
            //int total = Convert.ToInt32(lblTotal.Text) * PageSize;
            lblShowPage.Text = start + "件 から " + end + "件を表示 合計" + totalRecord + "件";

            lnkPagingNo1.BackColor = Color.Gainsboro;

            if (lblCurrent.Text.Equals(lblTotal.Text))
                lnkPagingGoNext.Enabled = false;
            else lnkPagingGoNext.Enabled = true;


            lnkPagingPrevMore.Visible = false;
            lnkPagingGoPrev.Enabled = false;

            int chkNextMore = Convert.ToInt32(lnkPagingNo5.Text);
            int total = Convert.ToInt32(lblTotal.Text);
            if (chkNextMore >= total)
                lnkPagingNextMore.Visible = false;
            else lnkPagingNextMore.Visible = true;

            selectedPageNoColor(lnkPagingNo1);
        }

        public void LinkButtonClick(String Name,int PageSize)
        {
            LinkButton lnk = this.FindControl(Name) as LinkButton;
            
            if (Name.Contains("PagingNo"))
            {
                txtpage.Text = null;
                lblCurrent.Text = lnk.Text;
                selectedPageNoColor(lnk);

                if (!lnk.Text.Equals("1"))
                    lnkPagingGoPrev.Enabled = true;
                else lnkPagingGoPrev.Enabled = false;

                if (lblCurrent.Text.Equals(lblTotal.Text))
                    lnkPagingGoNext.Enabled = false;
                else lnkPagingGoNext.Enabled = true;
            }

            else if (Name.Contains("lnkPagingNextMore"))
            {
                txtpage.Text = null;
                PagingNextMore();
            }

            else if (Name.Contains("lnkPagingGoNext"))
            {
                txtpage.Text = null;
                if (lblCurrent.Text.Equals(lnkPagingNo5.Text))
                    PagingNextMore();
                else
                {                    
                    int total = Convert.ToInt32(lblTotal.Text);
                    lblCurrent.Text = (Convert.ToInt32(lblCurrent.Text) + 1).ToString();
                    int i = Convert.ToInt32(lblCurrent.Text);
                    if (i >= total)
                    {
                        lnkPagingNextMore.Visible = false;
                        lnkPagingGoNext.Enabled = false;
                    }
                    else
                    {
                        lnkPagingNextMore.Visible = true;
                        lnkPagingGoNext.Enabled = true;
                    }
                }

                LinkButton lnkCur = getLinkButtonByText(lblCurrent.Text);

                if (!lnkCur.Text.Equals("1"))
                    lnkPagingGoPrev.Enabled = true;
                else lnkPagingGoPrev.Enabled = false;

                selectedPageNoColor(lnkCur);
            }

            else if (Name.Contains("lnkPagingLast"))
            {
                int total = Convert.ToInt32(lblTotal.Text);

                int LatestPageNo = total;
                //int remainder = total % PageSize;
                //if (remainder > 0 && remainder < PageSize)
                //    LatestPageNo++;

                lblCurrent.Text = lblTotal.Text;

                String pageIndex = (LatestPageNo % 5).ToString();

                if (pageIndex.Equals("1"))
                {
                    lnkPagingNo1.Text = total.ToString();
                    lnkPagingNo2.Text = (total + 1).ToString();
                    lnkPagingNo3.Text = (total + 2).ToString();
                    lnkPagingNo4.Text = (total + 3).ToString();
                    lnkPagingNo5.Text = (total + 4).ToString();

                    lnkPagingNo1.Visible = true;
                    lnkPagingNo2.Visible = false;
                    lnkPagingNo3.Visible = false;
                    lnkPagingNo4.Visible = false;
                    lnkPagingNo5.Visible = false;
                    lnkPagingNextMore.Visible = false;
                    lnkPagingGoNext.Enabled = false;
                    lnkPagingGoPrev.Enabled = true;
                    lnkPagingPrevMore.Visible = true;

                    selectedPageNoColor(lnkPagingNo1);
                }

                else if (pageIndex.Equals("2"))
                {
                    lnkPagingNo1.Text = (total-1).ToString();
                    lnkPagingNo2.Text = total.ToString();
                    lnkPagingNo3.Text = (total + 1).ToString();
                    lnkPagingNo4.Text = (total + 2).ToString();
                    lnkPagingNo5.Text = (total + 3).ToString();

                    lnkPagingNo1.Visible = true;
                    lnkPagingNo2.Visible = true;
                    lnkPagingNo3.Visible = false;
                    lnkPagingNo4.Visible = false;
                    lnkPagingNo5.Visible = false;
                    lnkPagingNextMore.Visible = false;
                    lnkPagingGoNext.Enabled = false;
                    lnkPagingGoPrev.Enabled = true;
                    lnkPagingPrevMore.Visible = true;

                    selectedPageNoColor(lnkPagingNo2);
                }
                else if (pageIndex.Equals("3"))
                {
                    lnkPagingNo1.Text = (total - 2).ToString();
                    lnkPagingNo2.Text = (total - 1).ToString();
                    lnkPagingNo3.Text = total.ToString();
                    lnkPagingNo4.Text = (total + 1).ToString();
                    lnkPagingNo5.Text = (total + 2).ToString();

                    lnkPagingNo1.Visible = true;
                    lnkPagingNo2.Visible = true;
                    lnkPagingNo3.Visible = true;
                    lnkPagingNo4.Visible = false;
                    lnkPagingNo5.Visible = false;
                    lnkPagingNextMore.Visible = false;
                    lnkPagingGoNext.Enabled = false;
                    lnkPagingGoPrev.Enabled = true;
                    lnkPagingPrevMore.Visible = true;

                    selectedPageNoColor(lnkPagingNo3);
                }
                else if (pageIndex.Equals("4"))
                {
                    lnkPagingNo1.Text = (total - 3).ToString();
                    lnkPagingNo2.Text = (total - 2).ToString();
                    lnkPagingNo3.Text = (total - 1).ToString();
                    lnkPagingNo4.Text = total.ToString();
                    lnkPagingNo5.Text = (total + 1).ToString();

                    lnkPagingNo1.Visible = true;
                    lnkPagingNo2.Visible = true;
                    lnkPagingNo3.Visible = true;
                    lnkPagingNo4.Visible = true;
                    lnkPagingNo5.Visible = false;
                    lnkPagingNextMore.Visible = false;
                    lnkPagingGoNext.Enabled = false;
                    lnkPagingGoPrev.Enabled = true;
                    lnkPagingPrevMore.Visible = true;

                    selectedPageNoColor(lnkPagingNo4);
                }
                else if (pageIndex.Equals("0"))
                {
                    lnkPagingNo1.Text = (total - 4).ToString();
                    lnkPagingNo2.Text = (total - 3).ToString();
                    lnkPagingNo3.Text = (total - 2).ToString();
                    lnkPagingNo4.Text = (total - 1).ToString();
                    lnkPagingNo5.Text = total.ToString();

                    lnkPagingNo1.Visible = true;
                    lnkPagingNo2.Visible = true;
                    lnkPagingNo3.Visible = true;
                    lnkPagingNo4.Visible = true;
                    lnkPagingNo5.Visible = true;
                    lnkPagingNextMore.Visible = false;
                    lnkPagingGoNext.Enabled = false;
                    lnkPagingGoPrev.Enabled = true;
                    lnkPagingPrevMore.Visible = true;

                    selectedPageNoColor(lnkPagingNo5);
                }
                if (lnkPagingNo1.Text.Equals("1"))
                    lnkPagingPrevMore.Visible = false;
                else lnkPagingPrevMore.Visible = true;

                LinkButton lnkCur = getLinkButtonByText(lblCurrent.Text);
                if (lnkCur.Text.Equals("1"))
                    lnkPagingGoPrev.Enabled = false;
                else lnkPagingGoPrev.Enabled = true;
            }

            else if (Name.Contains("lnkPagingFirst"))
            {                
                int total = Convert.ToInt32(lblTotalRecord.Text);
                
                CalculatePaging(total, PageSize, 1);
            }

            else if (Name.Contains("lnkPagingPrevMore"))
            {
                PagingPrevMore();
            }

            else if (Name.Contains("lnkPagingGoPrev"))
            {
                if (lblCurrent.Text.Equals(lnkPagingNo1.Text))
                    PagingPrevMore();
                else
                {                    
                    lblCurrent.Text = (Convert.ToInt32(lblCurrent.Text) - 1).ToString();
                    int i = Convert.ToInt32(lblCurrent.Text);

                    if (i == 1)
                    {
                        lnkPagingPrevMore.Visible = false;
                        lnkPagingGoPrev.Enabled = false;
                    }
                    else
                    {
                        lnkPagingPrevMore.Visible = true;
                        lnkPagingGoPrev.Enabled = true;
                    }
                }

                LinkButton lnkCur = getLinkButtonByText(lblCurrent.Text);
                
                if (!lnkCur.Text.Equals(lblTotal.Text))
                    lnkPagingGoNext.Enabled = true;
                else lnkPagingGoNext.Enabled = false;

                selectedPageNoColor(lnkCur);
            }
                //updated by KTA
            else if (Name.Contains("lnkPagingno"))
            {
                if (!String.IsNullOrWhiteSpace(txtpage.Text))
                {
                 
                    if (Convert.ToInt32(txtpage.Text) > Convert.ToInt32(lblTotal.Text))
                    {
                        //Exceedpageno();
                        GlobalUI.MessageBox(txtpage.Text + " " + "page not exist!");
                    }
                    else
                    {
                        lblCurrent.Text = txtpage.Text;
                        Pageno();
                    }
                }
                else 
                {
                    GlobalUI.MessageBox("Please fill page number!");
                }
            }//
            //lblShowPage.Text = lblCurrent.Text + "/" + lblTotal.Text;
            int curIndex = Convert.ToInt32(lblCurrent.Text);
            int start = ((curIndex - 1) * PageSize) + 1;
            int end = curIndex * PageSize;

            totalRecord = Convert.ToInt32(lblTotalRecord.Text);
            if (end > totalRecord)
                end = totalRecord;

            //lblShowPage.Text = lblCurrent.Text + "/" + lblTotal.Text;
            lblShowPage.Text = start + "件 から " + end + "件を表示 合計" + lblTotalRecord.Text + "件";
        }

        protected LinkButton getLinkButtonByText(String text)
        {
            if (text.Equals(lnkPagingNo1.Text))
                return lnkPagingNo1;
            else if (text.Equals(lnkPagingNo2.Text))
                return lnkPagingNo2;
            else if (text.Equals(lnkPagingNo3.Text))
                return lnkPagingNo3;
            else if (text.Equals(lnkPagingNo4.Text))
                return lnkPagingNo4;
            else
                return lnkPagingNo5;
        }

        protected void PagingPrevMore()
        {
            int i = Convert.ToInt32(lnkPagingNo1.Text) - 5;

            lnkPagingNo1.Text = (i).ToString();
            lnkPagingNo2.Text = (i + 1).ToString();
            lnkPagingNo3.Text = (i + 2).ToString();
            lnkPagingNo4.Text = (i + 3).ToString();
            lnkPagingNo5.Text = (i + 4).ToString();

            int total = Convert.ToInt32(lblTotal.Text);

            int Cur = Convert.ToInt32(lnkPagingNo1.Text);
            if (Cur <= total)
                lnkPagingNo1.Visible = true;

            Cur = Convert.ToInt32(lnkPagingNo2.Text);
            if (Cur <= total)
                lnkPagingNo2.Visible = true;

            Cur = Convert.ToInt32(lnkPagingNo3.Text);
            if (Cur <= total)
                lnkPagingNo3.Visible = true;

            Cur = Convert.ToInt32(lnkPagingNo4.Text);
            if (Cur <= total)
                lnkPagingNo4.Visible = true;

            Cur = Convert.ToInt32(lnkPagingNo5.Text);
            if (Cur <= total)
                lnkPagingNo5.Visible = true;

            lblCurrent.Text = lnkPagingNo5.Text;

            checkPrev();

            if (lnkPagingNo1.Text.Equals(lblTotal.Text))
                lnkPagingGoNext.Enabled = false;
            else lnkPagingGoNext.Enabled = true;

            selectedPageNoColor(lnkPagingNo5);

            int curPage = Convert.ToInt32(lnkPagingNo5.Text);
            if (curPage < total)
            {
                lnkPagingNextMore.Visible = true;
            }
            else lnkPagingNextMore.Visible = false;
        }

        protected void PagingNextMore()
        {
            lnkPagingGoPrev.Enabled = true;

            int i = Convert.ToInt32(lnkPagingNo1.Text) + 4;
            lnkPagingNo1.Text = (i + 1).ToString();
            lnkPagingNo2.Text = (i + 2).ToString();
            lnkPagingNo3.Text = (i + 3).ToString();
            lnkPagingNo4.Text = (i + 4).ToString();
            lnkPagingNo5.Text = (i + 5).ToString();

            lblCurrent.Text = lnkPagingNo1.Text;

            checkNext();

            if (lnkPagingNo1.Text.Equals(lblTotal.Text))
                lnkPagingGoNext.Enabled = false;
            else lnkPagingGoNext.Enabled = true;

            selectedPageNoColor(lnkPagingNo1);
        }

        protected void checkPrev()
        {
            if (lnkPagingNo1.Text.Equals("1"))
                lnkPagingPrevMore.Visible = false;
            else lnkPagingPrevMore.Visible = true;
        }

        protected void checkNext()
        {
            int total = Convert.ToInt32(lblTotal.Text);

            int i = Convert.ToInt32(lnkPagingNo1.Text);
            if (i == total)
                lnkPagingNextMore.Visible = false;
            else lnkPagingNextMore.Visible = true;

            i = Convert.ToInt32(lnkPagingNo2.Text);
            if (i > total)
                lnkPagingNo2.Visible = false;
            else lnkPagingNo2.Visible = true;

            if (i >= total)
                lnkPagingNextMore.Visible = false;
            else lnkPagingNextMore.Visible = true;


            i = Convert.ToInt32(lnkPagingNo3.Text);
            if (i > total)
                lnkPagingNo3.Visible = false;
            else lnkPagingNo3.Visible = true;

            if (i >= total)
                lnkPagingNextMore.Visible = false;
            else lnkPagingNextMore.Visible = true;


            i = Convert.ToInt32(lnkPagingNo4.Text);
            if (i > total)
                lnkPagingNo4.Visible = false;
            else lnkPagingNo4.Visible = true;

            if (i >= total)
                lnkPagingNextMore.Visible = false;
            else lnkPagingNextMore.Visible = true;


            i = Convert.ToInt32(lnkPagingNo5.Text);
            if (i > total)
                lnkPagingNo5.Visible = false;
            else lnkPagingNo5.Visible = true;

            if (i >= total)
                lnkPagingNextMore.Visible = false;
            else lnkPagingNextMore.Visible = true;

            if (i != 5)
            {
                lnkPagingPrevMore.Visible = true;
            }
            else lnkPagingPrevMore.Visible = false;
        }

        protected void selectedPageNoColor(LinkButton lnkCurrent)
        {
            for (int i = 1; i <= 5; i++)
            {
                LinkButton lnk = this.FindControl("lnkPagingNo" + i) as LinkButton;
                if (lnk==lnkCurrent)
                    lnk.BackColor = Color.Gainsboro;
                else lnk.BackColor = Color.White;
            }
        }

        //Created by KTA
        protected void Pageno() 
        {
             int total =0;
             if(!String.IsNullOrWhiteSpace(txtpage.Text))
                 total = Convert.ToInt32(txtpage.Text);
                 else
                 total = Convert.ToInt32(lblTotal.Text);


             int totals = Convert.ToInt32(lblTotal.Text);
             lblCurrent.Text = (Convert.ToInt32(lblCurrent.Text) + 1).ToString();
             int i = Convert.ToInt32(txtpage.Text);
             if (i >= totals)
             {
                 lnkPagingNextMore.Visible = false;
                 lnkPagingGoNext.Enabled = false;
             }
             else
             {
                 lnkPagingNextMore.Visible = true;
                 lnkPagingGoNext.Enabled = true;
             }
               int LatestPageNo = totals;
             
                string strno = Convert.ToString(total);
                lblCurrent.Text = strno;

            
               
                    if (LatestPageNo == total)
                    {
                        String pageIndex = (LatestPageNo % 5).ToString();

                        if (pageIndex.Equals("1"))
                        {
                            lnkPagingNo1.Text = total.ToString();
                            lnkPagingNo2.Text = (total + 1).ToString();
                            lnkPagingNo3.Text = (total + 2).ToString();
                            lnkPagingNo4.Text = (total + 3).ToString();
                            lnkPagingNo5.Text = (total + 4).ToString();

                            lnkPagingNo1.Visible = true;
                            lnkPagingNo2.Visible = false;
                            lnkPagingNo3.Visible = false;
                            lnkPagingNo4.Visible = false;
                            lnkPagingNo5.Visible = false;
                            lnkPagingNextMore.Visible = false;
                            lnkPagingGoNext.Enabled = false;
                            lnkPagingGoPrev.Enabled = true;
                            lnkPagingPrevMore.Visible = true;

                            selectedPageNoColor(lnkPagingNo1);
                        }

                        else if (pageIndex.Equals("2"))
                        {
                            lnkPagingNo1.Text = (total - 1).ToString();
                            lnkPagingNo2.Text = total.ToString();
                            lnkPagingNo3.Text = (total + 1).ToString();
                            lnkPagingNo4.Text = (total + 2).ToString();
                            lnkPagingNo5.Text = (total + 3).ToString();

                            lnkPagingNo1.Visible = true;
                            lnkPagingNo2.Visible = true;
                            lnkPagingNo3.Visible = false;
                            lnkPagingNo4.Visible = false;
                            lnkPagingNo5.Visible = false;
                            lnkPagingNextMore.Visible = false;
                            lnkPagingGoNext.Enabled = false;
                            lnkPagingGoPrev.Enabled = true;
                            lnkPagingPrevMore.Visible = true;

                            selectedPageNoColor(lnkPagingNo2);
                        }
                        else if (pageIndex.Equals("3"))
                        {
                            lnkPagingNo1.Text = (total - 2).ToString();
                            lnkPagingNo2.Text = (total - 1).ToString();
                            lnkPagingNo3.Text = total.ToString();
                            lnkPagingNo4.Text = (total + 1).ToString();
                            lnkPagingNo5.Text = (total + 2).ToString();

                            lnkPagingNo1.Visible = true;
                            lnkPagingNo2.Visible = true;
                            lnkPagingNo3.Visible = true;
                            lnkPagingNo4.Visible = false;
                            lnkPagingNo5.Visible = false;
                            lnkPagingNextMore.Visible = false;
                            lnkPagingGoNext.Enabled = false;
                            lnkPagingGoPrev.Enabled = true;
                            lnkPagingPrevMore.Visible = true;

                            selectedPageNoColor(lnkPagingNo3);
                        }
                        else if (pageIndex.Equals("4"))
                        {
                            lnkPagingNo1.Text = (total - 3).ToString();
                            lnkPagingNo2.Text = (total - 2).ToString();
                            lnkPagingNo3.Text = (total - 1).ToString();
                            lnkPagingNo4.Text = total.ToString();
                            lnkPagingNo5.Text = (total + 1).ToString();

                            lnkPagingNo1.Visible = true;
                            lnkPagingNo2.Visible = true;
                            lnkPagingNo3.Visible = true;
                            lnkPagingNo4.Visible = true;
                            lnkPagingNo5.Visible = false;
                            lnkPagingNextMore.Visible = false;
                            lnkPagingGoNext.Enabled = false;
                            lnkPagingGoPrev.Enabled = true;
                            lnkPagingPrevMore.Visible = true;

                            selectedPageNoColor(lnkPagingNo4);
                        }
                        else if (pageIndex.Equals("0"))
                        {
                            lnkPagingNo1.Text = (total - 4).ToString();
                            lnkPagingNo2.Text = (total - 3).ToString();
                            lnkPagingNo3.Text = (total - 2).ToString();
                            lnkPagingNo4.Text = (total - 1).ToString();
                            lnkPagingNo5.Text = total.ToString();

                            lnkPagingNo1.Visible = true;
                            lnkPagingNo2.Visible = true;
                            lnkPagingNo3.Visible = true;
                            lnkPagingNo4.Visible = true;
                            lnkPagingNo5.Visible = true;
                            lnkPagingNextMore.Visible = false;
                            lnkPagingGoNext.Enabled = false;
                            lnkPagingGoPrev.Enabled = true;
                            lnkPagingPrevMore.Visible = true;

                            selectedPageNoColor(lnkPagingNo5);
                        }
                        if (lnkPagingNo1.Text.Equals("1"))
                            lnkPagingPrevMore.Visible = false;
                        else lnkPagingPrevMore.Visible = true;

                        LinkButton lnkCur = getLinkButtonByText(lblCurrent.Text);
                        if (lnkCur.Text.Equals("1"))
                            lnkPagingGoPrev.Enabled = false;
                        else lnkPagingGoPrev.Enabled = true;
                    }///
                    else
                    {
                        String pageIndex = (total % 5).ToString();

                        
                        lnkPagingNo1.Visible = true;
                        lnkPagingNo2.Visible = true;
                        lnkPagingNo3.Visible = true;
                        lnkPagingNo4.Visible = true;
                        lnkPagingNo5.Visible = true;

                        #region

                        //if (pageIndex.Equals("1"))
                        //{
                        //    lnkPagingNo1.Text = total.ToString();

                        //    lnkPagingNo2.Text = (total + 1).ToString();
                        //    lnkPagingNo3.Text = (total + 2).ToString();
                        //    lnkPagingNo4.Text = (total + 3).ToString();
                        //    lnkPagingNo5.Text = (total + 4).ToString();
                        //    if (LatestPageNo == (total + 1))
                        //    {

                        //        lnkPagingNo3.Visible = false;
                        //        lnkPagingNo4.Visible = false;
                        //        lnkPagingNo5.Visible = false;
                        //        lnkPagingNextMore.Visible = false;
                        //    }
                        //    else if (LatestPageNo == (total + 2))
                        //    {


                        //        lnkPagingNo4.Visible = false;
                        //        lnkPagingNo5.Visible = false;
                        //        lnkPagingNextMore.Visible = false;
                        //    }
                        //    else if (LatestPageNo == (total + 3))
                        //    {

                        //        lnkPagingNo5.Visible = false;
                        //        lnkPagingNextMore.Visible = false;
                        //    }
                        //    else if (LatestPageNo == (total + 4))
                        //    {

                        //        lnkPagingNextMore.Visible = false;
                        //    }


                        //    selectedPageNoColor(lnkPagingNo1);
                        //}
                        //else if (pageIndex.Equals("2"))
                        //{
                        //    lnkPagingNo1.Text = (total - 1).ToString();
                        //    lnkPagingNo2.Text = total.ToString();
                        //    lnkPagingNo3.Text = (total + 1).ToString();
                        //    lnkPagingNo4.Text = (total + 2).ToString();
                        //    lnkPagingNo5.Text = (total + 3).ToString();

                        //    if ((total - 1) <= 0) 
                        //    {
                        //        lnkPagingNo1.Visible = false;
                        //    }
                        //    if (LatestPageNo == (total + 1))
                        //    {
                        //        lnkPagingNo4.Visible = false;
                        //        lnkPagingNo5.Visible = false;
                        //        lnkPagingNextMore.Visible = false;
                        //    }

                        //    else if (LatestPageNo == (total + 2))
                        //    {
                        //        lnkPagingNo5.Visible = false;
                        //        lnkPagingNextMore.Visible = false;
                        //    }

                        //    else if (LatestPageNo == (total + 3))
                        //    {
                        //        lnkPagingNextMore.Visible = false;
                        //    }
                        //    selectedPageNoColor(lnkPagingNo2);
                        //}


                        //else if (pageIndex.Equals("3"))
                        //{
                        //    lnkPagingNo1.Text = (total - 2).ToString();
                        //    lnkPagingNo2.Text = (total - 1).ToString();
                        //    lnkPagingNo3.Text = total.ToString();
                        //    lnkPagingNo4.Text = (total + 1).ToString();
                        //    lnkPagingNo5.Text = (total + 2).ToString();

                        //    if ((total - 2) <= 0)
                        //    {
                        //        lnkPagingNo1.Visible = false;
                        //    }
                        //    if ((total - 1)<=0)
                        //    {
                        //        lnkPagingNo2.Visible = false;
                        //    }
                        //    if (LatestPageNo == (total + 1))
                        //    {
                        //        lnkPagingNo5.Visible = false;
                        //        lnkPagingNextMore.Visible = false;
                        //    }
                        //    else if (LatestPageNo == (total + 2))
                        //    {
                        //        lnkPagingNextMore.Visible = false;
                        //    }

                        //    selectedPageNoColor(lnkPagingNo3);
                        //}
                        //else if (pageIndex.Equals("4"))
                        //{
                        //    lnkPagingNo1.Text = (total - 3).ToString();
                        //    lnkPagingNo2.Text = (total - 2).ToString();
                        //    lnkPagingNo3.Text = (total - 1).ToString();
                        //    lnkPagingNo4.Text = total.ToString();
                        //    lnkPagingNo5.Text = (total + 1).ToString();

                        //    if ((total - 3) <= 0)
                        //    {
                        //        lnkPagingNo1.Visible = false;
                        //    }
                        //    if ((total - 2) <= 0)
                        //    {
                        //        lnkPagingNo2.Visible = false;
                        //    }
                        //    if ((total - 1) <= 0)
                        //    {
                        //        lnkPagingNo3.Visible = false;
                        //    }
                        //    if (LatestPageNo == (total + 1))
                        //    {
                        //        lnkPagingNextMore.Visible = false;
                        //    }
                        //    selectedPageNoColor(lnkPagingNo4);
                        //}
                        //else if (pageIndex.Equals("0"))
                        //{
                        //    lnkPagingNo1.Text = (total - 4).ToString();
                        //    lnkPagingNo2.Text = (total - 3).ToString();
                        //    lnkPagingNo3.Text = (total - 2).ToString();
                        //    lnkPagingNo4.Text = (total - 1).ToString();
                        //    lnkPagingNo5.Text = total.ToString();
                        //    if ((total - 4) <= 0)
                        //    {
                        //        lnkPagingNo1.Visible = false;
                        //    }
                        //    if ((total - 3) <= 0)
                        //    {
                        //        lnkPagingNo2.Visible = false;
                        //    }
                        //    if ((total - 2) <= 0)
                        //    {
                        //        lnkPagingNo3.Visible = false;
                        //    }
                        //    if ((total - 1) <= 0)
                        //    {
                        //        lnkPagingNo4.Visible = false;
                        //    }

                        //    if (LatestPageNo == total)
                        //    {
                        //        lnkPagingNextMore.Visible = false;
                        //    }
                        //}
                        #endregion

                        #region
                        if (pageIndex.Equals("1"))
                        {
                            
                            lnkPagingNo1.Text = total.ToString();

                            lnkPagingNo2.Text = (total + 1).ToString();
                            lnkPagingNo3.Text = (total + 2).ToString();
                            lnkPagingNo4.Text = (total + 3).ToString();
                            lnkPagingNo5.Text = (total + 4).ToString();
                            if (LatestPageNo == (total + 1))
                            {

                                lnkPagingNo3.Visible = false;
                                lnkPagingNo4.Visible = false;
                                lnkPagingNo5.Visible = false;
                                lnkPagingNextMore.Visible = false;
                            }
                            else if (LatestPageNo == (total + 2))
                            {


                                lnkPagingNo4.Visible = false;
                                lnkPagingNo5.Visible = false;
                                lnkPagingNextMore.Visible = false;
                            }
                            else if (LatestPageNo == (total + 3))
                            {

                                lnkPagingNo5.Visible = false;
                                lnkPagingNextMore.Visible = false;
                            }
                            else if (LatestPageNo == (total + 4))
                            {

                                lnkPagingNextMore.Visible = false;
                            }


                            selectedPageNoColor(lnkPagingNo1);
                        }
                        else if (pageIndex.Equals("2"))
                        {
                            lnkPagingNo1.Text = (total - 1).ToString();
                            lnkPagingNo2.Text = total.ToString();
                            lnkPagingNo3.Text = (total + 1).ToString();
                            lnkPagingNo4.Text = (total + 2).ToString();
                            lnkPagingNo5.Text = (total + 3).ToString();

                            if ((total - 1) <= 0)
                            {
                                lnkPagingNo1.Visible = false;
                            }
                            if (LatestPageNo == (total + 1))
                            {
                                lnkPagingNo4.Visible = false;
                                lnkPagingNo5.Visible = false;
                                lnkPagingNextMore.Visible = false;
                            }

                            else if (LatestPageNo == (total + 2))
                            {
                                lnkPagingNo5.Visible = false;
                                lnkPagingNextMore.Visible = false;
                            }

                            else if (LatestPageNo == (total + 3))
                            {
                                lnkPagingNextMore.Visible = false;
                            }
                            selectedPageNoColor(lnkPagingNo2);
                        }


                        else if (pageIndex.Equals("3"))
                        {
                            lnkPagingNo1.Text = (total - 2).ToString();
                            lnkPagingNo2.Text = (total - 1).ToString();
                            lnkPagingNo3.Text = total.ToString();
                            lnkPagingNo4.Text = (total + 1).ToString();
                            lnkPagingNo5.Text = (total + 2).ToString();

                            if ((total - 2) <= 0)
                            {
                                lnkPagingNo1.Visible = false;
                            }
                            if ((total - 1) <= 0)
                            {
                                lnkPagingNo2.Visible = false;
                            }
                            if (LatestPageNo == (total + 1))
                            {
                                lnkPagingNo5.Visible = false;
                                lnkPagingNextMore.Visible = false;
                            }
                            else if (LatestPageNo == (total + 2))
                            {
                                lnkPagingNextMore.Visible = false;
                            }

                            selectedPageNoColor(lnkPagingNo3);
                        }
                        else if (pageIndex.Equals("4"))
                        {
                            lnkPagingNo1.Text = (total - 3).ToString();
                            lnkPagingNo2.Text = (total - 2).ToString();
                            lnkPagingNo3.Text = (total - 1).ToString();
                            lnkPagingNo4.Text = total.ToString();
                            lnkPagingNo5.Text = (total + 1).ToString();

                            if ((total - 3) <= 0)
                            {
                                lnkPagingNo1.Visible = false;
                            }
                            if ((total - 2) <= 0)
                            {
                                lnkPagingNo2.Visible = false;
                            }
                            if ((total - 1) <= 0)
                            {
                                lnkPagingNo3.Visible = false;
                            }
                            if (LatestPageNo == (total + 1))
                            {
                                lnkPagingNextMore.Visible = false;
                            }
                            selectedPageNoColor(lnkPagingNo4);
                        }
                        else if (pageIndex.Equals("0"))
                        {
                            lnkPagingNo1.Text = (total - 4).ToString();
                            lnkPagingNo2.Text = (total - 3).ToString();
                            lnkPagingNo3.Text = (total - 2).ToString();
                            lnkPagingNo4.Text = (total - 1).ToString();
                            lnkPagingNo5.Text = total.ToString();
                            if ((total - 4) <= 0)
                            {
                                lnkPagingNo1.Visible = false;
                            }
                            if ((total - 3) <= 0)
                            {
                                lnkPagingNo2.Visible = false;
                            }
                            if ((total - 2) <= 0)
                            {
                                lnkPagingNo3.Visible = false;
                            }
                            if ((total - 1) <= 0)
                            {
                                lnkPagingNo4.Visible = false;
                            }

                            if (LatestPageNo == total)
                            {
                                lnkPagingNextMore.Visible = false;
                            }
                            selectedPageNoColor(lnkPagingNo5);
                        }
                        #endregion
                        if (lnkPagingNo1.Text.Equals("1"))
                            lnkPagingPrevMore.Visible = false;
                        else lnkPagingPrevMore.Visible = true;

                        LinkButton lnkCur = getLinkButtonByText(lblCurrent.Text);
                        if (lnkCur.Text.Equals("1"))
                            lnkPagingGoPrev.Enabled = false;
                        else lnkPagingGoPrev.Enabled = true;

                    }
              
        }
        protected void Exceedpageno() 
        {
            int total = Convert.ToInt32(lblTotal.Text);

            int LatestPageNo = total;
          

            lblCurrent.Text = lblTotal.Text;

            String pageIndex = (LatestPageNo % 5).ToString();

            if (pageIndex.Equals("1"))
            {
                lnkPagingNo1.Text = total.ToString();
                lnkPagingNo2.Text = (total + 1).ToString();
                lnkPagingNo3.Text = (total + 2).ToString();
                lnkPagingNo4.Text = (total + 3).ToString();
                lnkPagingNo5.Text = (total + 4).ToString();

                lnkPagingNo1.Visible = true;
                lnkPagingNo2.Visible = false;
                lnkPagingNo3.Visible = false;
                lnkPagingNo4.Visible = false;
                lnkPagingNo5.Visible = false;
                lnkPagingNextMore.Visible = false;
                lnkPagingGoNext.Enabled = false;
                lnkPagingGoPrev.Enabled = true;
                lnkPagingPrevMore.Visible = true;

                selectedPageNoColor(lnkPagingNo1);
            }

            else if (pageIndex.Equals("2"))
            {
                lnkPagingNo1.Text = (total - 1).ToString();
                lnkPagingNo2.Text = total.ToString();
                lnkPagingNo3.Text = (total + 1).ToString();
                lnkPagingNo4.Text = (total + 2).ToString();
                lnkPagingNo5.Text = (total + 3).ToString();

                lnkPagingNo1.Visible = true;
                lnkPagingNo2.Visible = true;
                lnkPagingNo3.Visible = false;
                lnkPagingNo4.Visible = false;
                lnkPagingNo5.Visible = false;
                lnkPagingNextMore.Visible = false;
                lnkPagingGoNext.Enabled = false;
                lnkPagingGoPrev.Enabled = true;
                lnkPagingPrevMore.Visible = true;

                selectedPageNoColor(lnkPagingNo2);
            }
            else if (pageIndex.Equals("3"))
            {
                lnkPagingNo1.Text = (total - 2).ToString();
                lnkPagingNo2.Text = (total - 1).ToString();
                lnkPagingNo3.Text = total.ToString();
                lnkPagingNo4.Text = (total + 1).ToString();
                lnkPagingNo5.Text = (total + 2).ToString();

                lnkPagingNo1.Visible = true;
                lnkPagingNo2.Visible = true;
                lnkPagingNo3.Visible = true;
                lnkPagingNo4.Visible = false;
                lnkPagingNo5.Visible = false;
                lnkPagingNextMore.Visible = false;
                lnkPagingGoNext.Enabled = false;
                lnkPagingGoPrev.Enabled = true;
                lnkPagingPrevMore.Visible = true;

                selectedPageNoColor(lnkPagingNo3);
            }
            else if (pageIndex.Equals("4"))
            {
                lnkPagingNo1.Text = (total - 3).ToString();
                lnkPagingNo2.Text = (total - 2).ToString();
                lnkPagingNo3.Text = (total - 1).ToString();
                lnkPagingNo4.Text = total.ToString();
                lnkPagingNo5.Text = (total + 1).ToString();

                lnkPagingNo1.Visible = true;
                lnkPagingNo2.Visible = true;
                lnkPagingNo3.Visible = true;
                lnkPagingNo4.Visible = true;
                lnkPagingNo5.Visible = false;
                lnkPagingNextMore.Visible = false;
                lnkPagingGoNext.Enabled = false;
                lnkPagingGoPrev.Enabled = true;
                lnkPagingPrevMore.Visible = true;

                selectedPageNoColor(lnkPagingNo4);
            }
            else if (pageIndex.Equals("0"))
            {
                lnkPagingNo1.Text = (total - 4).ToString();
                lnkPagingNo2.Text = (total - 3).ToString();
                lnkPagingNo3.Text = (total - 2).ToString();
                lnkPagingNo4.Text = (total - 1).ToString();
                lnkPagingNo5.Text = total.ToString();

                lnkPagingNo1.Visible = true;
                lnkPagingNo2.Visible = true;
                lnkPagingNo3.Visible = true;
                lnkPagingNo4.Visible = true;
                lnkPagingNo5.Visible = true;
                lnkPagingNextMore.Visible = false;
                lnkPagingGoNext.Enabled = false;
                lnkPagingGoPrev.Enabled = true;
                lnkPagingPrevMore.Visible = true;

                selectedPageNoColor(lnkPagingNo5);
            }
            if (lnkPagingNo1.Text.Equals("1"))
                lnkPagingPrevMore.Visible = false;
            else lnkPagingPrevMore.Visible = true;

            LinkButton lnkCur = getLinkButtonByText(lblCurrent.Text);
            if (lnkCur.Text.Equals("1"))
                lnkPagingGoPrev.Enabled = false;
            else lnkPagingGoPrev.Enabled = true;
        
        }


    }
}      

                     
                          
                         
                          
                   

             
             
         
       