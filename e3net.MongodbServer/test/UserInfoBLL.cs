using e3net.MongodbServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e3net.MongodbServer.Test
{
      [MDbFactory("MDB_User_Pwd_Host_Port")]
    public class UserInfoBLL : MG_BaseDAL<UserInfo>, IMG_BaseDAL<UserInfo>
    {

          //public JsonResult GetList()
          //{

          //    int pageIndex = Request["page"] == null ? 1 : int.Parse(Request["page"]);
          //    int pageSize = Request["rows"] == null ? 10 : int.Parse(Request["rows"]);
          //    FilterDefinition<RMS_Buttons> filter = MongoSetTool.GetSql<RMS_Buttons>(Request["sqlSet"]);
          //    ////字段排序
          //    String sortField = Request["sort"];
          //    String sortOrder = Request["order"];

          //    PageClass pc = new PageClass();
          //    pc.sys_PageIndex = pageIndex;
          //    pc.sys_PageSize = pageSize;

          //    if (sortField != null)
          //    {
          //        pc.sys_Order = sortField;
          //    }
          //    else
          //    {
          //        pc.sys_Order = "Id";
          //    }

          //    if (sortOrder != null && sortOrder.Equals("asc"))
          //    {
          //        pc.isDescending = true;
          //    }
          //    else
          //    {
          //        pc.isDescending = true;
          //    }
          //    IList<RMS_Buttons> list2 = OPBiz.FindWithPagerM(filter, pc);
          //    Dictionary<string, object> dic = new Dictionary<string, object>();
          //    dic.Add("rows", list2);
          //    dic.Add("total", pc.RCount);
          //    return Json(dic, JsonRequestBehavior.AllowGet);
          //}


        //public JsonResult EditInfo(RMS_Menus Mode)
        //{
        //    Random rnd = new Random();
        //    bool IsAdd = false;
        //    if (!(Mode.Id != null && !Mode.Id.ToString().Equals("00000000-0000-0000-0000-000000000000")))//id为空，是添加
        //    {
        //        IsAdd = true;
        //    }
        //    if (IsAdd)
        //    {
        //        //Mode.Id = Guid.NewGuid().ToString("N"); 
        //        Mode.CreateTime = DateTime.Now;
        //        Mode.ModifyTime = DateTime.Now;
        //        Mode.IsEnable = true;
        //        Mode.IsShow = true;
        //        MDBiz.Insert(Mode);

        //        return Json("ok", JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        //  spmodel.GroupId = GroupId;
        //        Mode.CreateTime = DateTime.Now;
        //        Mode.ModifyTime = DateTime.Now;
        //        if (MDBiz.UpdateM(Mode.Id, Mode))
        //        {
        //            return Json("ok", JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            return Json("Nok", JsonRequestBehavior.AllowGet);
        //        }

        //    }

        //}
        //public JsonResult GetInfo(string ID)
        //{
        //    RMS_Menus Rmodel = MDBiz.FindByIDM(ID);
        //    //  groupsBiz.Add(rol);
        //    return Json(Rmodel, JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult GetOneOut(string ManuId)//获取菜单未添加的按钮
        //{
        //    //DataSet ds = OPBiz.ExecuteSqlToDataSet(" select * from RMS_Buttons where Id not in( select Id from Function_Buttons where Id='" + ManuId + "')");

        //    var queryH = MBDBiz.GetQueryable(p => p.ManuId == ManuId, p => p.ManuId).Select(p => p.ButtonId).ToList();//已经添加的按钮
        //    var query = BDBiz.GetQueryable(p => !queryH.Contains(p.Id), d => d.Id);//没有的


        //    List<RMS_Buttons> Rmodel = query.ToList();
        //    return Json(Rmodel, JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult GetOneIn(string ManuId)//获取菜单已经添加的按钮
        //{
        //    var queryH = v_MBDBiz.GetQueryable(p => p.ManuId == ManuId, p => p.ManuId);//已经添加的按钮
        //    List<V_MenuButtons> Rmodel = queryH.ToList();
        //    return Json(Rmodel, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult GetJson()
        //{

        //    List<RMS_Menus> listAll = MDBiz.Getcollection().AsQueryable().ToList();
        //    List<TreeMenus> listTree = MDBiz.GetTreeManus(listAll);
        //    return Json(listTree);
        //}
        //public JsonResult DeleteInfo(string ID)
        //{


        //    bool res = MDBiz.DeleteM(ID);
        //    if (res)
        //    {
        //        return Json("OK", JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {

        //        return Json("Nok", JsonRequestBehavior.AllowGet);
        //    }


        //}
        ///// <summary>
        ///// //添加单按钮
        ///// </summary>
        ///// <param name="btnId"></param>
        ///// <param name="ManuId"></param>
        ///// <returns></returns>
        //public JsonResult AddManuBtn(string BtnId, string ManuId, string OrderNo)
        //{
        //    FilterDefinition<RMS_MenuButtons> filter = Builders<RMS_MenuButtons>.Filter.And(Builders<RMS_MenuButtons>.Filter.Eq("ButtonId", BtnId), Builders<RMS_MenuButtons>.Filter.Eq("ManuId", ManuId));
        //    RMS_MenuButtons item = MBDBiz.FindSingle(filter);
        //    if (item != null)
        //    {
        //        item.OrderNo = int.Parse(OrderNo);
        //        //  spmodel.GroupId = GroupId;
        //        if (MBDBiz.UpdateM(item.Id, item))
        //        {
        //            return Json("OK", JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    item = new RMS_MenuButtons();
        //    //item.Id = Guid.NewGuid().ToString("N");
        //    item.ButtonId = BtnId;
        //    item.ManuId = ManuId;
        //    item.OrderNo = int.Parse(OrderNo);
        //    MBDBiz.Insert(item);
        //    return Json("OK", JsonRequestBehavior.AllowGet);

        //}
        ///// <summary>
        ///// //删菜单按钮
        ///// </summary>
        ///// <param name="btnId"></param>
        ///// <param name="ManuId"></param>
        ///// <returns></returns>
        //public JsonResult DelManuBtn(string IdSet, string ManuId)
        //{
        //    List<string> ids = IdSet.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        //    bool f = MBDBiz.DeleteBatchM(ids);
        //    return Json("OK", JsonRequestBehavior.AllowGet);
        //}

    }
}
