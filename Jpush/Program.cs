using cn.jpush.api;
using cn.jpush.api.common;
using cn.jpush.api.common.resp;
using cn.jpush.api.push.mode;
using cn.jpush.api.push.notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jpush
{
    class Program
    {
        public static String TITLE = "Test from C# v3 sdk";
        public static String ALERT = "Test from c#";
        public static String MSG_CONTENT = "Test from C# v3 sdk - msgContent";
        public static String REGISTRATION_ID = "0900e8d85ef";
        public static String SMSMESSAGE = "Test from C# v3 sdk - SMSMESSAGE";
        public static int DELAY_TIME = 1;
        public static String TAG = "tag_api";
        public static String app_key = "84ba5339afae8e4696197f11";
        public static String master_secret = "2dfed9415fc32885cadd7522";

        static void Main(string[] args)
        {
            Console.WriteLine("*****开始发送******");
            JPushClient client = new JPushClient(app_key, master_secret);
            string[] tags = { "01234567890005"};
            PushPayload payload = PushObject_all_tag_alert(tags);
            try
            {
                var result = client.SendPush(payload);
                //由于统计数据并非非是即时的,所以等待一小段时间再执行下面的获取结果方法
                System.Threading.Thread.Sleep(10000);
                //如需查询上次推送结果执行下面的代码
                var apiResult = client.getReceivedApi(result.msg_id.ToString());
                var apiResultv3 = client.getReceivedApi_v3(result.msg_id.ToString());
            }
            catch (APIRequestException e)
            {
                Console.WriteLine("Error response from JPush server. Should review and fix it. ");
                Console.WriteLine("HTTP Status: " + e.Status);
                Console.WriteLine("Error Code: " + e.ErrorCode);
                Console.WriteLine("Error Message: " + e.ErrorMessage);
            }
            catch (APIConnectionException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }


        /// <summary>
        /// 推送给所有人员
        /// </summary>
        /// <returns></returns>
        public static PushPayload PushObject_All_All_Alert()
        {
            PushPayload pushPayload = new PushPayload();
            pushPayload.platform = Platform.all();
            pushPayload.audience = Audience.all();
            pushPayload.notification = new Notification().setAlert(ALERT);
            return pushPayload;
        }

        /// <summary>
        /// 推送给指定别名人员
        /// </summary>
        /// <returns></returns>
        public static PushPayload PushObject_all_alias_alert(string[] alias)
        {
            PushPayload pushPayload_alias = new PushPayload();
            pushPayload_alias.platform = Platform.all();
            pushPayload_alias.audience = Audience.s_alias(alias);
            pushPayload_alias.notification = new Notification().setAlert(ALERT);
            return pushPayload_alias;
        }

        /// <summary>
        /// 推送给指定tag人员
        /// </summary>
        /// <returns></returns>
        public static PushPayload PushObject_all_tag_alert(string[] tags)
        {
            PushPayload pushPayload_alias = new PushPayload();
            pushPayload_alias.platform = Platform.all();
            pushPayload_alias.audience = Audience.s_tag(tags);
            pushPayload_alias.notification = new Notification().setAlert(ALERT);
            return pushPayload_alias;
        }

        /// <summary>
        /// 推送附加内容
        /// </summary>
        /// <returns></returns>
        public static PushPayload PushObject_ios_tagAnd_alertWithExtrasAndMessage()
        {
            PushPayload pushPayload = new PushPayload();
            pushPayload.platform = Platform.android_ios();
            pushPayload.audience = Audience.s_tag_and("tag1", "tag_all");
            var notification = new Notification();
            notification.IosNotification = new IosNotification().setAlert(ALERT)
                                                                .setBadge(5)
                                                                .setSound("happy")
                                                                .AddExtra("from","JPush");
            pushPayload.notification = notification;
            pushPayload.message = Message.content(MSG_CONTENT);
            return pushPayload;
        }
    }
}
