﻿using eBarDatabase.Interfaces;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using ViewModels;

namespace eBarDatabase
{
    public class RestaurantEventOperations : IRestaurantEventOperations
    {
        private DBModels _databaseEntities;
        private IDatabaseLogger _logger;
        public RestaurantEventOperations()
        {
            _logger = new DatabaseLogger();
            _databaseEntities = new DBModels();
        }
        public List<RestaurantEvent> GetRestaurantEvents(int restaurantId)
        {
            return _databaseEntities.RestaurantEvent.Where(x => x.RestaurantId == restaurantId).ToList();
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
                return message = RestaurantEventMessage.NokUpdate;
            }
            return message;
        }
    }
}