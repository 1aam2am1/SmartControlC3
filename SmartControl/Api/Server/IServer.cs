using SmartControl.Api.Server.Queries;
using SmartControl.Api.Server.Responses;
using SmartControl.Api.Server.SaveQueries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartControl.Api.Server
{
    public interface IServer
    {
        /// <summary>
        /// Authorize and login to server
        /// </summary>
        /// <param name="s">Connection Settings</param>
        /// <param name="i">Login Settings. Login and Password</param>
        /// <returns></returns>
        public Task<bool> Auth(IConnectSettings s, ILoginSettings i);

        #region GetData

        /// <summary>
        /// Get Version of device
        /// </summary>
        /// <returns></returns>
        public Task<VersionResponse> Version();

        /// <summary>
        /// Gut information of status of device
        /// </summary>
        /// <returns></returns>
        public Task<StatusResponse> GetFullStatus();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">Function that will execute on message received
        /// 0 No change
        /// 1 Change
        /// 2 Error connection lost
        /// 3 Closed <see cref="ClosePingClient"/>
        /// </param>
        public void StartAsyncPing(Action<int> v);

        /// <summary>
        /// Close Ping Client
        /// </summary>
        public void ClosePingClient();

        /// <summary>
        /// Get List of Parameters
        /// </summary>
        /// <param name="parameter">List of Parameters to get</param>
        /// <returns></returns>
        public Task<ParameterResponse> GetParameters(ParameterQuery parameter);

        /// <summary>
        /// Get Calendar Configuration
        /// </summary>
        /// <returns></returns>
        public Task<CalendarResponse> GetCalendarData();

        /// <summary>
        /// Get All Task in day
        /// </summary>
        /// <param name="day">With day to load</param>
        /// <returns></returns>
        public Task<CalendarDayResponse> GetTask(CalendarDayQuery day);

        /// <summary>
        /// Get working modes of device
        /// </summary>
        /// <returns></returns>
        public Task<ModesResponse> GetModes();

        /// <summary>
        /// Get Historical Data
        /// </summary>
        /// <param name="history">Time period and parameters to get</param>
        /// <returns></returns>
        public Task<HistoryResponse> GetHistoricalData(HistoryQuery history);

        #endregion

        #region SaveData

        /// <summary>
        /// Data to save on device
        /// </summary>
        /// <param name="parameter">List of parameters to save</param>
        /// <returns></returns>
        public Task<OkErrorResponce> SaveParameters(ParameterSave parameter);

        /// <summary>
        /// Data of activation of calendar to send to device.
        /// </summary>
        /// <param name="calendar">Calendar Data</param>
        /// <returns></returns>
        public Task<OkErrorResponce> SaveCalendar(CalendarSave calendar);

        /// <summary>
        /// Save List of Tasks in each day
        /// </summary>
        /// <param name="calendar">List of tasks</param>
        /// <returns></returns>
        public Task<OkErrorResponce> SaveDayTask(CalendarDaySave calendar);

        /// <summary>
        /// Modes to send
        /// </summary>
        /// <param name="modes">List of modes</param>
        /// <returns></returns>
        public Task<OkErrorResponce> SaveModes(ModesSave modes);

        #endregion
    }
}
