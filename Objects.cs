using System;
using System.Collections.Generic;

namespace WXShare
{
    public class Objects
    {
        /// <summary>
        /// 活动
        /// </summary>
        public class Activity
        {
            public Activity() { }
            // 系统编号
            public string id;
            // 开始时间
            public DateTime timeStart;
            // 结束时间
            public DateTime timeEnd;
            // 标题
            public string title;
            // 内容HTML
            public string content;
            // 模板编号
            public int template;
            // 简介
            public string brief;
            // 是否有效
            public bool valid;
            // 预览图
            public string imgSrc;
            // 模板附加参数
            public string templateAddition;
        }
        /// <summary>
        /// 活动报名
        /// </summary>
        public class ActivitySign
        {
            public ActivitySign() { }
            // 系统编号
            public string id;
            // 姓名
            public string name;
            // 手机号
            public string phone;
            // 区县
            public string location;
            // 详细地址
            public string locationDetail;
            // 活动ID
            public string activityID;
            // 分享人手机
            public string shareSource;
            // 报名时间
            public DateTime signDate;
        }
        /// <summary>
        /// 公司
        /// </summary>
        public class Company
        {
            public Company() { }
            // 系统编号
            public string id;
            // 微信号
            public string gh;
            // appID
            public string appID;
            // secretID
            public string appSecret;
            // token
            public string token;
        }
        /// <summary>
        /// 订单
        /// </summary>
        public class Order
        {
            public Order() { }
            // 系统编号
            public string id;
            // 客户姓名
            public string name;
            // 手机号码
            public string phone;
            // 建单时间
            public DateTime createTime;
            // 接单时间
            public DateTime orderTime;
            // 目前状态
            public int status;
            // 区县
            public string location;
            // 详细地址
            public string locationDetail;
            // 节点渠道
            public int orderChannel;
            // 涂刷类型
            public int brushType;
            // 涂刷需求
            public string brushDemand;
            // 客户专员
            public string commissioner;
            // 专员预约时间
            public DateTime comOrderTime;
            // 订单取消原因
            public string canceledReason;
            // 计划基检时间
            public DateTime comCheckOrderTime;
            // 实际基检时间
            public DateTime comCheckTime;
            // 房屋用途
            public int housePurpose;
            // 房屋类型
            public int houseType;
            // 签约日期
            public DateTime signTime;
            // 派工日期
            public DateTime dispatchTime;
            // 签约不成功原因
            public string signFailedReason;
            // 合同号
            public string contractNumber;
            // 施工队
            public string constructionTeam;
            // 计划开工日期
            public DateTime workOrderDate;
            // 计划完工日期
            public DateTime workCompleteOrderDate;
            // 退单原因
            public string refuseReason;
            // 实际开工日期
            public DateTime workDate;
            // 实际完工日期
            public DateTime workCompleteDate;
            // 停工天数
            public int workStopDays;
            // 计划工期
            public int timeLimitOrder;
            // 实际工期
            public int timeLimit;
            // 主材金额
            public double mmSum;
            // 辅材金额
            public double smSum;
            // 施工金额
            public double workSum;
            // 合同签订
            public bool signed;
            // 收据提供
            public bool receipted;
            // 最满意部分
            public string likePart;
            // 最不满意部分
            public string dislikePart;
            // NPS
            public int NPS;
            // 完工回访时间
            public DateTime callbackTime;
            // 质保卡编号
            public string qaNumber;
            // 寄件日期
            public DateTime postDate;
            // 年度
            public DateTime year;
            // 是否回访成功
            public bool callbackSuccess;
            // 回访不成功原因
            public string callbackFailedReason;
            // 跟踪回访备注
            public string callbackTrace;
            // 满意度回访备注
            public string callbackComm;
            // 快递单号
            public string postNumber;
            // 房屋结构
            public int houseStructure;
            // 建筑面积
            public int mianJi;
            // 内墙
            public int neiQiang;
            // 艺术漆
            public int yiShuQi;
            // 外墙
            public int waiQiang;
            // 阳光
            public int yangTai;
            // 木器
            public int muQi;
            // 铁艺
            public int tieYi;
            // 优惠来源
            public int youHuiLaiYuan;
        }
        /// <summary>
        /// 模板
        /// </summary>
        public class Template
        {
            public Template() { }
            // 系统编号
            public string id;
            // 模板名称
            public string name;
        }
        /// <summary>
        /// 用户（除经销商）
        /// </summary>
        public class User
        {
            public User() { }
            // 系统编号
            public string id;
            // 手机号
            public string phone;
            // 密码
            public string password;
            // 角色/身份
            public string identity;
            // 姓名
            public string name;
            // 身份证
            public string IDCard;
        }
        /// <summary>
        /// 施工队
        /// </summary>
        public class Team
        {
            // 施工队名称
            public string teamName;
            // 施工队id
            public string id;
            // 施工队成员
            public List<User> members;
        }
        /// <summary>
        /// 经销商
        /// </summary>
        public class User2
        {
            public User2() { }
            // 系统编号
            public string id;
            // 手机号
            public string phone;
            // 密码
            public string password;
            // 角色
            public string identity;
            // 姓名
            public string name;
            // 身份证
            public string IDCard;
            // 区县
            public string location;
            // 详细地址
            public string detailLocation;
            // 资质图片
            public string signImage;
        }
        /// <summary>
        /// 订单辅助基础类
        /// </summary>
        public class OrderBase
        {
            // 系统编号
            public string id;
            // 相应显示名
            public string view;
        }
        /// <summary>
        /// 涂刷需求
        /// </summary>
        public class BrushDemand : OrderBase { }
        /// <summary>
        /// 涂刷类型
        /// </summary>
        public class BrushType : OrderBase { }
        /// <summary>
        /// 订单来源
        /// </summary>
        public class Channel : OrderBase { }
        /// <summary>
        /// 房屋用途
        /// </summary>
        public class HousePurpose : OrderBase { }
        /// <summary>
        /// 房屋结构
        /// </summary>
        public class HouseStructure : OrderBase { }
        /// <summary>
        /// 房屋类型
        /// </summary>
        public class HouseType : OrderBase { }
        /// <summary>
        /// 订单状态
        /// </summary>
        public class Status : OrderBase { }
    }
}