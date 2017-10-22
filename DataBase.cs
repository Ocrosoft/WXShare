using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WXShare
{
    public class DataBase
    {
        /// <summary>
        /// 用户数据库操作
        /// </summary>
        public class User
        {
            /// <summary>
            /// 添加用户（包含UnAuth）
            /// </summary>
            /// <param name="user"></param>
            /// <returns></returns>
            public static bool Add(Objects.User user, bool authed = false)
            {
                if (authed || user.identity == "1")
                {
                    string sql = "insert into users(name, phone, identity) values(?name, ?phone, ?iden);";
                    MySqlParameter[] para = new MySqlParameter[3];
                    para[0] = new MySqlParameter("?name", user.name);
                    para[1] = new MySqlParameter("?phone", user.phone);
                    para[2] = new MySqlParameter("?iden", user.identity);

                    int ret = MySQLHelper.ExecuteNonQuery(sql, para);
                    if (ret == 1)
                    {
                        return true;
                    }
                    return false;
                }
                // 业务员-施工队-管理员
                else if (user.identity == "2" || user.identity == "4" || user.identity == "5")
                {
                    string sql = "insert into users_unsigned(name, phone, identity, IDCard) values(?name, ?phone, ?iden, ?IDCard);";
                    MySqlParameter[] para = new MySqlParameter[4];
                    para[0] = new MySqlParameter("?name", user.name);
                    para[1] = new MySqlParameter("?phone", user.phone);
                    para[2] = new MySqlParameter("?iden", user.identity);
                    para[3] = new MySqlParameter("?IDCard", user.IDCard);

                    int ret = MySQLHelper.ExecuteNonQuery(sql, para);
                    if (ret == 1)
                    {
                        return true;
                    }
                    return false;
                }
                else
                {
                    return false;
                }
            }
            /// <summary>
            /// 添加经销商
            /// </summary>
            /// <param name="user"></param>
            /// <returns></returns>
            public static bool Add2(Objects.User2 user)
            {
                return false;
            }
            /// <summary>
            /// 登录（phone, password, identity）
            /// </summary>
            /// <param name="user"></param>
            /// <returns></returns>
            public static bool Login(Objects.User user)
            {
                string sql = "select count(*) from users where phone=?phone and password=?pass and identity=?iden;";
                MySqlParameter[] para = new MySqlParameter[3];
                para[0] = new MySqlParameter("?phone", user.phone);
                para[1] = new MySqlParameter("?pass", user.password);
                para[2] = new MySqlParameter("?iden", user.identity);

                Object ret = MySQLHelper.ExecuteScalar(sql, para);
                if (Int32.Parse(ret.ToString()) == 1)
                {
                    return true;
                }
                return false;
            }
            /// <summary>
            /// 用户是否存在（phone, identity）
            /// </summary>
            /// <param name="user"></param>
            /// <returns></returns>
            public static bool Exits(Objects.User user)
            {
                string sql = "select count(*) from users where phone=?phone and identity=?iden;";
                MySqlParameter[] para = new MySqlParameter[2];
                para[0] = new MySqlParameter("?phone", user.phone);
                para[1] = new MySqlParameter("?iden", user.identity);

                Object ret = MySQLHelper.ExecuteScalar(sql, para);
                if (Int32.Parse(ret.ToString()) == 1)
                {
                    return true;
                }
                return false;
            }
            /// <summary>
            /// 获取用户信息（phone, identity）
            /// </summary>
            /// <param name="info"></param>
            /// <returns></returns>
            public static Objects.User Get(Objects.User info)
            {
                string sql = "select id, phone, password, identity, name, IDCard from users where phone = ?phone and identity = ?iden";
                MySqlParameter[] para = new MySqlParameter[2];
                para[0] = new MySqlParameter("?phone", info.phone);
                para[1] = new MySqlParameter("?iden", info.identity);

                var ds = MySQLHelper.ExecuteDataSet(sql, para);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    return null;
                }
                var array = ds.Tables[0].Rows[0].ItemArray;
                return new Objects.User()
                {
                    id = array[0].ToString(),
                    phone = array[1].ToString(),
                    password = array[2].ToString(),
                    identity = array[3].ToString(),
                    name = array[4].ToString(),
                    IDCard = array[5].ToString()
                };
            }
            /// <summary>
            /// 修改密码
            /// </summary>
            /// <param name="user"></param>
            /// <returns></returns>
            public static bool ModifyPassword(Objects.User user)
            {
                string sql = "update users set password = ?pass where id = ?id";
                MySqlParameter[] para = new MySqlParameter[2];
                para[0] = new MySqlParameter("?pass", user.password);
                para[1] = new MySqlParameter("?id", user.id);

                int ret = MySQLHelper.ExecuteNonQuery(sql, para);
                if (ret == 1)
                {
                    return true;
                }
                return false;
            }
            public static bool Delete(Objects.User user)
            {
                return false;
            }
        }
        /// <summary>
        /// 需审核用户数据库操作
        /// </summary>
        public class UserUnAuth
        {
            // Add()包含在User中
            /// <summary>
            /// 获取用户信息（id）
            /// </summary>
            /// <param name="info"></param>
            /// <returns></returns>
            public static Objects.User Get(Objects.User info)
            {
                string sql = "select id, phone, password, identity, name, IDCard from users_unsigned where id = ?id";
                MySqlParameter para = new MySqlParameter("?id", info.id);

                var ds = MySQLHelper.ExecuteDataSet(sql, para);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    return null;
                }
                var array = ds.Tables[0].Rows[0].ItemArray;
                return new Objects.User()
                {
                    id = array[0].ToString(),
                    phone = array[1].ToString(),
                    password = array[2].ToString(),
                    identity = array[3].ToString(),
                    name = array[4].ToString(),
                    IDCard = array[5].ToString()
                };
            }
            /// <summary>
            /// 进行审核（id）
            /// </summary>
            /// <param name="info"></param>
            /// <returns></returns>
            public static bool Auth(Objects.User info)
            {
                var user = Get(info);
                if (User.Add(user, true))
                {
                    return Delete(info);
                }
                return false;
            }
            /// <summary>
            /// 获取所有待审核信息
            /// </summary>
            /// <returns></returns>
            public static List<Objects.User> Gets()
            {
                string sql = "select id, phone, password, identity, name, IDCard from users_unsigned";

                var ds = MySQLHelper.ExecuteDataSet(sql);
                List<Objects.User> ret = new List<Objects.User>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var array = row.ItemArray;
                    ret.Add(new Objects.User()
                    {
                        id = array[0].ToString(),
                        phone = array[1].ToString(),
                        password = array[2].ToString(),
                        identity = array[3].ToString(),
                        name = array[4].ToString(),
                        IDCard = array[5].ToString()
                    });
                }
                return ret;
            }
            /// <summary>
            /// 获取某个角色的所有待审核信息
            /// </summary>
            /// <param name="info"></param>
            /// <returns></returns>
            public static List<Objects.User> Gets(Objects.User info)
            {
                string sql = "select id, phone, password, identity, name, IDCard from users_unsigned where identity = ?iden";
                MySqlParameter para = new MySqlParameter("?iden", info.identity);

                var ds = MySQLHelper.ExecuteDataSet(sql, para);
                List<Objects.User> ret = new List<Objects.User>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var array = row.ItemArray;
                    ret.Add(new Objects.User()
                    {
                        id = array[0].ToString(),
                        phone = array[1].ToString(),
                        password = array[2].ToString(),
                        identity = array[3].ToString(),
                        name = array[4].ToString(),
                        IDCard = array[5].ToString()
                    });
                }
                return ret;
            }
            /// <summary>
            /// 删除（id）
            /// </summary>
            /// <param name="info"></param>
            /// <returns></returns>
            public static bool Delete(Objects.User info)
            {
                string sql = "delete from users_unsigned where id = ?id";
                MySqlParameter para = new MySqlParameter("?id", info.id);

                int ret = MySQLHelper.ExecuteNonQuery(sql, para);
                if (ret == 1)
                {
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 活动数据库操作
        /// </summary>
        public class Activity
        {
            /// <summary>
            /// 添加活动
            /// </summary>
            /// <param name="activity"></param>
            /// <returns></returns>
            public static bool Add(Objects.Activity activity)
            {
                string sql = "insert into activity(timeStart, timeEnd, title, content, templet, brief, valid)" +
                    "values(?timeStart, ?timeEnd, ?title, ?content, ?templet, ?brief, ?valid)";
                MySqlParameter[] para = new MySqlParameter[9];
                para[0] = new MySqlParameter("?timeStart", activity.timeStart);
                para[1] = new MySqlParameter("?timeEnd", activity.timeEnd);
                para[2] = new MySqlParameter("?title", activity.title);
                para[3] = new MySqlParameter("?content", activity.content);
                para[4] = new MySqlParameter("?templet", activity.template);
                para[5] = new MySqlParameter("?brief", activity.brief);
                para[6] = new MySqlParameter("?valid", activity.valid ? "1" : "0");
                para[7] = new MySqlParameter("imgSrc", activity.imgSrc);
                para[8] = new MySqlParameter("templateAddition", activity.templateAddition);

                int ret = MySQLHelper.ExecuteNonQuery(sql, para);
                if (ret == 1)
                {
                    return true;
                }
                return false;
            }
            /// <summary>
            /// 获取活动（id）
            /// </summary>
            /// <param name="info"></param>
            /// <returns></returns>
            public static Objects.Activity Get(Objects.Activity info)
            {
                string sql = "select * from activity where id = ?id";
                MySqlParameter para = new MySqlParameter("?id", info.id);

                var ds = MySQLHelper.ExecuteDataSet(sql, para);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    return null;
                }
                var array = ds.Tables[0].Rows[0].ItemArray;
                return new Objects.Activity()
                {
                    id = array[0].ToString(),
                    timeStart = DateTime.Parse(array[1].ToString()),
                    timeEnd = DateTime.Parse(array[2].ToString()),
                    title = array[3].ToString(),
                    content = array[4].ToString(),
                    template = int.Parse(array[5].ToString()),
                    brief = array[6].ToString(),
                    valid = array[7].ToString() == "1",
                    imgSrc = array[8].ToString(),
                    templateAddition = array[9].ToString()
                };
            }
            /// <summary>
            /// 获取所有有效活动
            /// </summary>
            /// <returns></returns>
            public static List<Objects.Activity> Gets()
            {
                string sql = "select * from activity where timeStart<now() and timeEnd>now() and valid = 1";
                List<Objects.Activity> ret = new List<Objects.Activity>();

                var ds = MySQLHelper.ExecuteDataSet(sql);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var array = row.ItemArray;
                    ret.Add(new Objects.Activity()
                    {
                        id = array[0].ToString(),
                        timeStart = DateTime.Parse(array[1].ToString()),
                        timeEnd = DateTime.Parse(array[2].ToString()),
                        title = array[3].ToString(),
                        content = array[4].ToString(),
                        template = int.Parse(array[5].ToString()),
                        brief = array[6].ToString(),
                        valid = array[7].ToString() == "1",
                        imgSrc = array[8].ToString(),
                        templateAddition = array[9].ToString()
                    });
                }
                return ret;
            }
            /// <summary>
            /// 获取所有活动（管理员）
            /// </summary>
            /// <returns></returns>
            public static List<Objects.Activity> GetsAll()
            {
                string sql = "select * from activity";
                List<Objects.Activity> ret = new List<Objects.Activity>();

                var ds = MySQLHelper.ExecuteDataSet(sql);
                foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                {
                    var array = row.ItemArray;
                    ret.Add(new Objects.Activity()
                    {
                        id = array[0].ToString(),
                        timeStart = DateTime.Parse(array[1].ToString()),
                        timeEnd = DateTime.Parse(array[2].ToString()),
                        title = array[3].ToString(),
                        content = array[4].ToString(),
                        template = int.Parse(array[5].ToString()),
                        brief = array[6].ToString(),
                        valid = array[7].ToString() == "1",
                        imgSrc = array[8].ToString(),
                        templateAddition = array[9].ToString()
                    });
                }
                return ret;
            }
            /// <summary>
            /// 修改活动
            /// </summary>
            /// <param name="activity"></param>
            /// <returns></returns>
            public static bool Modify(Objects.Activity activity)
            {
                string sql = "update activity set timeStart = ?timeStart, timeEnd = ?timeEnd, title = ?title, content = ?content, templet = ?templet, brief = ?brief, valid = ?valid, imgSrc = ?imgSrc, templateAddition = ?templateAddition where id = ?id";
                MySqlParameter[] para = new MySqlParameter[10];
                para[0] = new MySqlParameter("?timeStart", activity.timeStart.ToString("yyyy-MM-dd HH:mm:ss"));
                para[1] = new MySqlParameter("?timeEnd", activity.timeEnd.ToString("yyyy-MM-dd HH:mm:ss"));
                para[2] = new MySqlParameter("?title", activity.title);
                para[3] = new MySqlParameter("?content", activity.content);
                para[4] = new MySqlParameter("?templet", activity.template.ToString());
                para[5] = new MySqlParameter("?brief", activity.brief);
                para[6] = new MySqlParameter("?valid", activity.valid ? "1" : "0");
                para[7] = new MySqlParameter("?imgSrc", activity.imgSrc);
                para[8] = new MySqlParameter("?id", activity.id);
                para[9] = new MySqlParameter("?templateAddition", activity.templateAddition);

                int ret = MySQLHelper.ExecuteNonQuery(sql, para);
                if (ret == 1)
                {
                    return true;
                }
                return false;
            }
            public static bool Delete()
            {
                return false;
            }
        }
        public class ActivitySign
        {
            /// <summary>
            /// 添加报名信息
            /// </summary>
            /// <param name="acs">注：时间属性无效</param>
            /// <returns></returns>
            public static bool Add(Objects.ActivitySign acs)
            {
                string sql = "insert into activitysign(name, phone, location, locationDetail, activityID, shareSource, signDate) " +
                    "values(?name, ?phone, ?location, ?locationDetail, ?activityID, ?shareSource, now());";
                MySqlParameter[] para = new MySqlParameter[6];
                para[0] = new MySqlParameter("?name", acs.name);
                para[1] = new MySqlParameter("?phone", acs.phone);
                para[2] = new MySqlParameter("?location", acs.location);
                para[3] = new MySqlParameter("?locationDetail", acs.locationDetail);
                para[4] = new MySqlParameter("?activityID", acs.activityID);
                para[5] = new MySqlParameter("?shareSource", acs.shareSource);

                int ret = MySQLHelper.ExecuteNonQuery(sql, para);
                if (ret == 1)
                {
                    return true;
                }
                return false;
            }
            /// <summary>
            /// 获取报名信息（id）
            /// </summary>
            /// <param name="info"></param>
            /// <returns></returns>
            public static Objects.ActivitySign Get(Objects.ActivitySign info)
            {
                string sql = "select id, name, phone, location, locationDetail, activityID, shareSource, signDate from activitysign where id = ?id";
                MySqlParameter para = new MySqlParameter("?id", info.id);

                var ds = MySQLHelper.ExecuteDataSet(sql, para);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    return null;
                }
                var array = ds.Tables[0].Rows[0].ItemArray;
                return new Objects.ActivitySign()
                {
                    id = array[0].ToString(),
                    name = array[1].ToString(),
                    phone = array[2].ToString(),
                    location = array[3].ToString(),
                    locationDetail = array[4].ToString(),
                    activityID = array[5].ToString(),
                    shareSource = array[6].ToString(),
                    signDate = DateTime.Parse(array[7].ToString())
                };
            }
            /// <summary>
            /// 获取所有报名信息
            /// </summary>
            /// <returns></returns>
            public static List<Objects.ActivitySign> Gets()
            {
                string sql = "select * from activitySign";
                List<Objects.ActivitySign> ret = new List<Objects.ActivitySign>();

                var ds = MySQLHelper.ExecuteDataSet(sql);
                foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                {
                    var array = row.ItemArray;
                    ret.Add(new Objects.ActivitySign()
                    {
                        id = array[0].ToString(),
                        name = array[1].ToString(),
                        phone = array[2].ToString(),
                        location = array[3].ToString(),
                        locationDetail = array[4].ToString(),
                        activityID = array[5].ToString(),
                        shareSource = array[6].ToString(),
                        signDate = DateTime.Parse(array[7].ToString())
                    });
                }
                return ret;
            }
            /// <summary>
            /// 查询指定活动id的报名信息
            /// </summary>
            /// <param name="info">活动对象</param>
            /// <returns></returns>
            public static List<Objects.ActivitySign> Gets(Objects.Activity info)
            {
                string sql = "select * from activitySign where activityID = ?id";
                MySqlParameter para = new MySqlParameter("?id", info.id);
                List<Objects.ActivitySign> ret = new List<Objects.ActivitySign>();

                var ds = MySQLHelper.ExecuteDataSet(sql, para);
                foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                {
                    var array = row.ItemArray;
                    ret.Add(new Objects.ActivitySign()
                    {
                        id = array[0].ToString(),
                        name = array[1].ToString(),
                        phone = array[2].ToString(),
                        location = array[3].ToString(),
                        locationDetail = array[4].ToString(),
                        activityID = array[5].ToString(),
                        shareSource = array[6].ToString(),
                        signDate = DateTime.Parse(array[7].ToString())
                    });
                }
                return ret;
            }
            /// <summary>
            /// 查询指定分享来源id的报名信息
            /// </summary>
            /// <param name="info"></param>
            /// <returns></returns>
            public static List<Objects.ActivitySign> Gets(Objects.User info)
            {
                string sql = "select * from activitySign where shareSource = ?uid";
                MySqlParameter para = new MySqlParameter("?uid", info.phone);
                List<Objects.ActivitySign> ret = new List<Objects.ActivitySign>();

                var ds = MySQLHelper.ExecuteDataSet(sql, para);
                foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                {
                    var array = row.ItemArray;
                    ret.Add(new Objects.ActivitySign()
                    {
                        id = array[0].ToString(),
                        name = array[1].ToString(),
                        phone = array[2].ToString(),
                        location = array[3].ToString(),
                        locationDetail = array[4].ToString(),
                        activityID = array[5].ToString(),
                        shareSource = array[6].ToString(),
                        signDate = DateTime.Parse(array[7].ToString())
                    });
                }
                return ret;
            }
            public static bool Modify(Objects.ActivitySign acs)
            {
                return false;
            }
            public static bool Delete()
            {
                return false;
            }
        }
        public class Template
        {
            /// <summary>
            /// 获取所有模板
            /// </summary>
            /// <returns></returns>
            public static List<Objects.Template> Gets()
            {
                string sql = "select id, name from template";
                List<Objects.Template> ret = new List<Objects.Template>();

                var ds = MySQLHelper.ExecuteDataSet(sql);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var array = row.ItemArray;
                    ret.Add(new Objects.Template()
                    {
                        id = array[0].ToString(),
                        name = array[1].ToString()
                    });
                }
                return ret;
            }
        }
        public class Order
        {
            /// <summary>
            /// 增加记录（name, phone, createTime，location，locationDetail）
            /// </summary>
            /// <param name="order"></param>
            /// <returns></returns>
            public static bool Add(Objects.Order order)
            {
                string sql = "insert into orders(name, phone, createTime, orderTime, location, locationDetail) values(?na, ?ph, ?ct, ?lo, ?lod, now())";
                MySqlParameter[] para = new MySqlParameter[5];
                para[0] = new MySqlParameter("?na", order.name);
                para[1] = new MySqlParameter("?ph", order.phone);
                para[2] = new MySqlParameter("?ct", order.createTime.ToString("yy-MM-dd hh:mm:ss"));
                para[3] = new MySqlParameter("?lo", order.location);
                para[4] = new MySqlParameter("?lod", order.locationDetail);

                int ret = MySQLHelper.ExecuteNonQuery(sql, para);
                if(ret == 1)
                {
                    return true;
                }
                return false;
            }
            /// <summary>
            /// 删除记录（id）
            /// </summary>
            /// <param name="order"></param>
            /// <returns></returns>
            public static bool Delete(Objects.Order order)
            {
                string sql = "delete from orders where id = ?id";
                MySqlParameter para = new MySqlParameter("?id", order.id);

                int ret = MySQLHelper.ExecuteNonQuery(sql, para);
                if(ret == 1)
                {
                    return true;
                }
                return false;
            }
            /// <summary>
            /// 派单给专员（commissioner）
            /// </summary>
            /// <param name="order"></param>
            /// <returns></returns>
            public static bool ToCommissioner(Objects.Order order)
            {
                string sql = "update orders set(commissioner = ?comm) where id = ?id";
                MySqlParameter[] para = new MySqlParameter[2];
                para[0] = new MySqlParameter("?comm", order.commissioner);
                para[1] = new MySqlParameter("?id", order.id);

                int ret = MySQLHelper.ExecuteNonQuery(sql, para);
                if(ret == 1)
                {
                    return true;
                }
                return false;
            }
            /// <summary>
            /// 专员与客户预约（brushType，brushDemand，comOrderTime, comCheckOrderTime）
            /// </summary>
            /// <param name="order"></param>
            /// <returns></returns>
            public static bool CommissionerOrderToCustomer(Objects.Order order)
            {
                string sql = "update orders set(brushType = ?bt, brushDemand = ?bd, " +
                    "comOrderTime = ?cot, comCheckOrderTime = ?ccot) where id = ?id";
                MySqlParameter[] para = new MySqlParameter[5];
                para[0] = new MySqlParameter("?br",order.brushType);
                para[1] = new MySqlParameter("?bd",order.brushDemand);
                para[2] = new MySqlParameter("?cot", order.comOrderTime.ToString("yy-MM-dd hh:mm:ss"));
                para[3] = new MySqlParameter("?ccot", order.comCheckOrderTime.ToString("yy-MM-dd hh:mm:ss"));
                para[4] = new MySqlParameter("?id", order.id);

                int ret = MySQLHelper.ExecuteNonQuery(sql, para);
                if (ret == 1)
                {
                    return true;
                }
                return false;
            }
            /// <summary>
            /// 取消订单（canceledReason）
            /// </summary>
            /// <param name="order"></param>
            /// <returns></returns>
            public static bool CancelOrder(Objects.Order order)
            {
                string sql = "update orders set(canceledReason = ?cr) where id = ?id";
                MySqlParameter[] para = new MySqlParameter[2];
                para[0] = new MySqlParameter("?cr", order.canceledReason);
                para[1] = new MySqlParameter("?id", order.id);

                int ret = MySQLHelper.ExecuteNonQuery(sql, para);
                if (ret == 1)
                {
                    return true;
                }
                return false;
            }
            /// <summary>
            /// 专员进行基检（comCheckTime）
            /// </summary>
            /// <param name="order"></param>
            /// <returns></returns>
            public static bool CommissionerCheck(Objects.Order order)
            {
                string sql = "update orders set(comCheckTime = ?cct) where id = ?id";
                MySqlParameter[] para = new MySqlParameter[2];
                para[0] = new MySqlParameter("?cct", order.comCheckTime.ToString("yy-MM-dd hh:mm:ss"));
                para[1] = new MySqlParameter("?id", order.id);

                int ret = MySQLHelper.ExecuteNonQuery(sql, para);
                if (ret == 1)
                {
                    return true;
                }
                return false;
            }
            /// <summary>
            /// 基检完成（housePurpose, houseType, houseStructure, 
            /// mianJi, neiQiang, yiShuQi, waiQiang, yangTai, muQi, tieYi, youHuiLaiYuan）
            /// </summary>
            /// <param name="order"></param>
            /// <returns></returns>
            public static bool CommissionerCheckComplete(Objects.Order order)
            {
                string sql = "update orders set(housePurpose = ?hp, houseType = ?ht, houseStructure = ?hs, " +
                    "mianJi = ?mj, neiQiang = ?nq, yiShuQi = ?ysq, waiQiang = ?wq, yangTai = ?yt, " +
                    "muQi = ?mq, tieYi = ?ty, youHuiLaiYuan = ?yhly) where id = ?id";
                MySqlParameter[] para = new MySqlParameter[13];
                para[0] = new MySqlParameter("?hp", order.housePurpose);
                para[1] = new MySqlParameter("?ht", order.houseType);
                para[3] = new MySqlParameter("?hs", order.houseStructure);
                para[4] = new MySqlParameter("?mj", order.mianJi);
                para[5] = new MySqlParameter("?nq", order.neiQiang);
                para[6] = new MySqlParameter("?ysq", order.yiShuQi);
                para[7] = new MySqlParameter("?wq", order.waiQiang);
                para[8] = new MySqlParameter("?yt", order.yangTai);
                para[9] = new MySqlParameter("?mq", order.muQi);
                para[10] = new MySqlParameter("?ty", order.tieYi);
                para[11] = new MySqlParameter("?yhly", order.youHuiLaiYuan);
                para[12] = new MySqlParameter("?id", order.id);

                int ret = MySQLHelper.ExecuteNonQuery(sql, para);
                if (ret == 1)
                {
                    return true;
                }
                return false;
            }
            /// <summary>
            /// 签约（constractNumber）
            /// </summary>
            /// <param name="order"></param>
            /// <returns></returns>
            public static bool SignTheOrder(Objects.Order order)
            {
                string sql = "update orders set(signTime = now(), constractNumber = ?cn) where id = ?id";
                MySqlParameter[] para = new MySqlParameter[2];
                para[0] = new MySqlParameter("?cn", order.contractNumber);
                para[1] = new MySqlParameter("?id", order.id);

                int ret = MySQLHelper.ExecuteNonQuery(sql, para);
                if (ret == 1)
                {
                    return true;
                }
                return false;
            }
            /// <summary>
            /// 签约失败（signFailedReason）
            /// </summary>
            /// <param name="order"></param>
            /// <returns></returns>
            public static bool SignFailed(Objects.Order order)
            {
                string sql = "update orders set(signFailedReason = ?sfr) where id = ?id";
                MySqlParameter[] para = new MySqlParameter[2];
                para[0] = new MySqlParameter("?sfr", order.signFailedReason);
                para[1] = new MySqlParameter("?id", order.id);

                int ret = MySQLHelper.ExecuteNonQuery(sql, para);
                if (ret == 1)
                {
                    return true;
                }
                return false;
            }
            /// <summary>
            /// 派工
            /// </summary>
            /// <param name="order"></param>
            /// <returns></returns>
            public static bool Dispatch(Objects.Order order)
            {
                string sql = "update orders set(constructionTeam = ?ct, workOrderDate = ?wod, " +
                    "workCompleteOrderDate = ?wcod, timeLimit = ?tl) where id = ?id";
                MySqlParameter[] para = new MySqlParameter[5];
                para[0] = new MySqlParameter("?ct", order.constructionTeam);
                para[1] = new MySqlParameter("?wod", order.workOrderDate.ToString("yy-MM-dd"));
                para[2] = new MySqlParameter("?wcod", order.workCompleteOrderDate.ToString("yy-MM-dd"));
                para[3] = new MySqlParameter("?tl", order.timeLimit);
                para[4] = new MySqlParameter("?id", order.id);

                int ret = MySQLHelper.ExecuteNonQuery(sql, para);
                if (ret == 1)
                {
                    return true;
                }
                return false;
            }
            /// <summary>
            /// 拒单（refuseReason）
            /// </summary>
            /// <param name="order"></param>
            /// <returns></returns>
            public static bool RefuseOrder(Objects.Order order)
            {
                string sql = "update orders set(refuseReason = ?rr) where id = ?id";
                MySqlParameter[] para = new MySqlParameter[2];
                para[0] = new MySqlParameter("?rr", order.refuseReason);
                para[1] = new MySqlParameter("?id", order.id);

                int ret = MySQLHelper.ExecuteNonQuery(sql, para);
                if (ret == 1)
                {
                    return true;
                }
                return false;
            }
            /// <summary>
            /// 开工
            /// </summary>
            /// <param name="order"></param>
            /// <returns></returns>
            public static bool StartWork(Objects.Order order)
            {
                string sql = "update orders set(workDate = now()) where id = ?id";
                MySqlParameter para = new MySqlParameter("?id", order.id);

                int ret = MySQLHelper.ExecuteNonQuery(sql, para);
                if (ret == 1)
                {
                    return true;
                }
                return false;
            }
        }
        public class OrderBase
        {
            /// <summary>
            /// 添加记录
            /// </summary>
            /// <param name="obj">OrderBase任意衍生类</param>
            /// <param name="tableName">衍生类对应的数据库表名</param>
            /// <returns></returns>
            public virtual bool Add(Objects.OrderBase obj, string tableName)
            {
                string sql = "insert into " + tableName + "(View) values(?view)";
                MySqlParameter para = new MySqlParameter("?view", obj.view);

                int ret = MySQLHelper.ExecuteNonQuery(sql, para);
                if (ret == 1)
                {
                    return true;
                }
                return false;
            }
            /// <summary>
            /// 删除记录（记录名）
            /// </summary>
            /// <param name="obj"></param>
            /// <param name="tableName"></param>
            /// <returns></returns>
            public virtual bool Delete(Objects.OrderBase obj, string tableName)
            {
                string sql = "delete from " + tableName + " where View = ?view";
                MySqlParameter para = new MySqlParameter("?view", obj.view);

                int ret = MySQLHelper.ExecuteNonQuery(sql, para);
                if (ret >= 1)
                {
                    return true;
                }
                return false;
            }
            /// <summary>
            /// 修改（id）
            /// </summary>
            /// <param name="obj"></param>
            /// <param name="tableName"></param>
            /// <returns></returns>
            public virtual bool Modify(Objects.OrderBase obj, string tableName)
            {
                string sql = "update " + tableName + " set(View = ?view) where Id = ?id";
                MySqlParameter[] para = new MySqlParameter[2];
                para[0] = new MySqlParameter("?view", obj.view);
                para[1] = new MySqlParameter("?id", obj.id);

                int ret = MySQLHelper.ExecuteNonQuery(sql, para);
                if (ret == 1)
                {
                    return true;
                }
                return false;
            }
            /// <summary>
            /// 获取记录（id）
            /// </summary>
            /// <param name="obj"></param>
            /// <param name="tableName"></param>
            /// <returns></returns>
            public virtual Objects.OrderBase Get(Objects.OrderBase obj, string tableName)
            {
                string sql = "select View from " + tableName + " where Id = ?id";
                MySqlParameter para = new MySqlParameter("?id", obj.id);

                Object ret = MySQLHelper.ExecuteScalar(sql, para);
                obj.view = ret.ToString();
                return obj;
            }
            /// <summary>
            /// 获取所有记录
            /// </summary>
            /// <param name="tableName"></param>
            /// <returns></returns>
            public virtual List<Objects.OrderBase> Gets(string tableName)
            {
                string sql = "select * from " + tableName;
                List<Objects.OrderBase> ret = new List<Objects.OrderBase>();

                var ds = MySQLHelper.ExecuteDataSet(sql);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var array = row.ItemArray;
                    ret.Add(new Objects.OrderBase()
                    {
                        id = array[0].ToString(),
                        view = array[1].ToString()
                    });
                }
                return ret;
            }
        }
        public class OrderBaseS : OrderBase
        {
            public string tableName;
            public OrderBaseS(string tableName)
            {
                this.tableName = tableName;
            }
            public bool Add(Objects.OrderBase obj)
            {
                return Add(obj, tableName);
            }
            public bool Delete(Objects.OrderBase obj)
            {
                return Delete(obj, tableName);
            }
            public bool Modify(Objects.OrderBase obj)
            {
                return Modify(obj, tableName);
            }
            public Objects.OrderBase Get(Objects.OrderBase obj)
            {
                return Get(obj, tableName);
            }
            public virtual List<Objects.OrderBase> Gets()
            {
                return Gets(tableName);
            }
        }
        public class BrushDemand : OrderBaseS
        {
            public BrushDemand() : base("order_brushdemand") { }
            public Objects.BrushDemand Get(Objects.BrushDemand obj)
            {
                return (Objects.BrushDemand)base.Get(obj);
            }
            public new List<Objects.BrushDemand> Gets()
            {
                var list = base.Gets();
                List<Objects.BrushDemand> ret = new List<Objects.BrushDemand>();
                foreach(var each in list)
                {
                    ret.Add((Objects.BrushDemand)each);
                }
                return ret;
            }
        }
        public class BrushType : OrderBaseS
        {
            public BrushType() : base("order_brushtype") { }
            public Objects.BrushType Get(Objects.BrushType obj)
            {
                return (Objects.BrushType)base.Get(obj);
            }
            public new List<Objects.BrushType> Gets()
            {
                var list = base.Gets();
                List<Objects.BrushType> ret = new List<Objects.BrushType>();
                foreach (var each in list)
                {
                    ret.Add((Objects.BrushType)each);
                }
                return ret;
            }
        }
        public class Channel : OrderBaseS
        {
            public Channel() : base("order_channel") { }
            public Objects.Channel Get(Objects.Channel obj)
            {
                return (Objects.Channel)base.Get(obj);
            }
            public new List<Objects.Channel> Gets()
            {
                var list = base.Gets();
                List<Objects.Channel> ret = new List<Objects.Channel>();
                foreach (var each in list)
                {
                    ret.Add((Objects.Channel)each);
                }
                return ret;
            }
        }
        public class HousePurpose : OrderBaseS
        {
            public HousePurpose() : base("order_channel") { }
            public Objects.HousePurpose Get(Objects.HousePurpose obj)
            {
                return (Objects.HousePurpose)base.Get(obj);
            }
            public new List<Objects.HousePurpose> Gets()
            {
                var list = base.Gets();
                List<Objects.HousePurpose> ret = new List<Objects.HousePurpose>();
                foreach (var each in list)
                {
                    ret.Add((Objects.HousePurpose)each);
                }
                return ret;
            }
        }
        public class HouseStructure : OrderBaseS
        {
            public HouseStructure() : base("order_channel") { }
            public Objects.HouseStructure Get(Objects.HouseStructure obj)
            {
                return (Objects.HouseStructure)base.Get(obj);
            }
            public new List<Objects.HouseStructure> Gets()
            {
                var list = base.Gets();
                List<Objects.HouseStructure> ret = new List<Objects.HouseStructure>();
                foreach (var each in list)
                {
                    ret.Add((Objects.HouseStructure)each);
                }
                return ret;
            }
        }
        public class HouseType : OrderBaseS
        {
            public HouseType() : base("order_channel") { }
            public Objects.HouseType Get(Objects.HouseType obj)
            {
                return (Objects.HouseType)base.Get(obj);
            }
            public new List<Objects.HouseType> Gets()
            {
                var list = base.Gets();
                List<Objects.HouseType> ret = new List<Objects.HouseType>();
                foreach (var each in list)
                {
                    ret.Add((Objects.HouseType)each);
                }
                return ret;
            }
        }
        public class Status : OrderBaseS
        {
            public Status() : base("order_channel") { }
            public Objects.Status Get(Objects.Status obj)
            {
                return (Objects.Status)base.Get(obj);
            }
            public new List<Objects.Status> Gets()
            {
                var list = base.Gets();
                List<Objects.Status> ret = new List<Objects.Status>();
                foreach (var each in list)
                {
                    ret.Add((Objects.Status)each);
                }
                return ret;
            }
        }
    }
}