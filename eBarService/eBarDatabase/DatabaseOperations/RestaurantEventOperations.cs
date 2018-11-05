using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using ViewModels;

namespace eBarDatabase
{
    public class RestaurantEventOperations : IRestaurantEventOperations
    {
        private IDatabaseLogger _logger;
        public RestaurantEventOperations()
        {
            _logger = new DatabaseLogger();
        }

        public List<RestaurantEvent> GetMostRecent10Restaurants()
        {
            List<RestaurantEvent> events = new List<RestaurantEvent>();

            using (var context = new DBModels())
            {

                if (context.Database.Connection.State == System.Data.ConnectionState.Closed || context.Database.Connection.State == System.Data.ConnectionState.Broken)
                {
                    context.Database.Connection.Open();
                }
                events = context.RestaurantEvent.OrderByDescending(x => x.EventPublicationDate).Take(10).ToList();
            }

            return events;
        }

        public List<RestaurantEvent> GetRestaurantEvents(int userId)
        {
            List<RestaurantEvent> events = new List<RestaurantEvent>();

            try
            {
                using (var context = new DBModels())
                {

                    if (context.Database.Connection.State == System.Data.ConnectionState.Closed || context.Database.Connection.State == System.Data.ConnectionState.Broken)
                    {
                        context.Database.Connection.Open();
                    }
                    events = (from resEvent in context.RestaurantEvent
                              join restAdmin in context.RestaurantAdministrators
                              on resEvent.RestaurantId equals restAdmin.RestaurantId
                              where restAdmin.UserID == userId
                              select resEvent).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Log("GetRestaurantEvents_Exception", ex.Message);
            }

            return events;
        }

        public List<RestaurantEventModel> GetRestaurantEventsForTimeline(DateTime lastEventDate)
        {
            List<RestaurantEventModel> events = new List<RestaurantEventModel>();

            using (var context = new DBModels())
            {

                if (context.Database.Connection.State == System.Data.ConnectionState.Closed || context.Database.Connection.State == System.Data.ConnectionState.Broken)
                {
                    context.Database.Connection.Open();
                }
                events = (from ev in context.RestaurantEvent
                          join rest in context.Restaurants on ev.RestaurantId equals rest.RestaurantId
                          where ev.EventPublicationDate < lastEventDate
                          orderby ev.EventPublicationDate descending
                          select new RestaurantEventModel
                          {
                              RestaurantId = rest.RestaurantId,
                              EventDescription = ev.EventDescription,
                              EventEndDate = ev.EventEndDate.ToString(),
                              EventPublicationDate = ev.EventPublicationDate,
                              EventStartDate = ev.EventStartDate.ToString(),
                              EventTitle = ev.EventTitle,
                              RestaurantEventID = ev.RestaurantEventID,
                              RestaurantName = rest.RestaurantName
                          }).Take(10).ToList();
            }
            return events;
        }

        public string SaveRestaurantEvent(RestaurantEvent restaurantEvent)
        {
            string saveMessage;
            try
            {
                using (var context = new DBModels())
                {
                    context.RestaurantEvent.Add(restaurantEvent);
                    context.SaveChanges();
                    saveMessage = RestaurantEventMessage.OkMessage;
                }
            }
            catch (Exception ex)
            {
                saveMessage = RestaurantEventMessage.NokMessage;
                _logger.Log("SaveRestaurantEvent_Exception", ex.Message);
            }
            return saveMessage;
        }
        public string UpdateRestaurantEvent(RestaurantEvent restaurantEvent)
        {
            string message;
            try
            {
                using (var context = new DBModels())
                {
                    var eventRest = context.RestaurantEvent.FirstOrDefault(x => x.RestaurantEventID == restaurantEvent.RestaurantEventID);
                    if (eventRest == null)
                    {
                        message = RestaurantEventMessage.NokUpdate;
                    }
                    else
                    {
                        eventRest.EventTitle = restaurantEvent.EventTitle;
                        eventRest.EventDescription = restaurantEvent.EventDescription;
                        eventRest.EventStartDate = restaurantEvent.EventStartDate;
                        eventRest.EventEndDate = restaurantEvent.EventEndDate;
                        context.Entry(eventRest).State = EntityState.Modified;
                        context.SaveChanges();
                        message = RestaurantEventMessage.OkUpdate;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Log("UpdateRestaurantEvent_Exception", ex.Message);
                return message = RestaurantEventMessage.NokUpdate;
            }
            return message;
        }
    }
}
