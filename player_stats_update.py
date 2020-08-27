import mysql.connector
import datetime

class player_stats:
    def __init__(self,host,user,passwd,database):
        self.db = mysql.connector.connect(host=host,
                                     user=user,
                                     passwd=passwd,
                                     database=database)


        self.cursor = self.db.cursor()

    def update_chest_info(self,data,player_id , action):
        query = ""
        self.cursor.execute("SELECT chest_info FROM PLAYER_INFO WHERE id = \"" +player_id+ "\"" )
        chest_details_ = ""
        for i in self.cursor:
            chest_details_ = str(i[0])

        chest_details = []
        for i in chest_details_:
            chest_details.append(i)
        if action == "start" :
            chest_n = int(data)
            time_ = datetime.datetime.now()

            string_insert  = str(time_.month) + "/" +str(time_.day) + "/" + str(time_.year) + " "+ str(time_.hour)+":"+str(time_.minute)+":"+str(time_.second)
            chest_true , chest_state , chest_date_time = tuple(chest_details_.split('_'))
            chest_date_time_each = chest_date_time.split(',')
            chest_state_Each = chest_state.split(',')
            chest_state_Each[chest_n] = 'p'
            chest_date_time_each[chest_n] = string_insert
            chest_Time_updated = ""
            for i in chest_date_time_each:
                chest_Time_updated = chest_Time_updated + i + ","

            chest_State_updated = ""
            for j in chest_state_Each:
                chest_State_updated = chest_State_updated + j+","

            chest_State_updated = chest_State_updated[:-1]
            chest_Time_updated = chest_Time_updated[:-1]

            query = chest_true + "_" + chest_State_updated + "_" + chest_Time_updated


        if action == "open":
            chest_n = int(data)
            chest_true, chest_state, chest_date_time = tuple(chest_details_.split('_'))
            chest_state_Each = chest_state.split(',')
            chest_state_Each[chest_n] = 'd'
            chest_State_updated = ""
            for j in chest_state_Each:
                chest_State_updated = chest_State_updated + j + ","

            chest_State_updated = chest_State_updated[:-1]

            query = chest_true + "_" + chest_State_updated + "_" + chest_date_time

        if action == "remove":
            chest_n = int(data)

            chest_true , chest_state , chest_date_time = tuple(chest_details_.split('_'))
            chest_true_each = chest_true.split(',')
            chest_date_time_each = chest_date_time.split(',')
            chest_state_Each = chest_state.split(',')
            chest_true_each[chest_n] = '0'
            chest_state_Each[chest_n] = 'n'
            chest_date_time_each[chest_n] = '0'

            chest_Time_updated = ""
            for i in chest_date_time_each:
                chest_Time_updated = chest_Time_updated + i + ","

            chest_State_updated = ""
            for j in chest_state_Each:
                chest_State_updated = chest_State_updated + j+","

            chest_true_updated = ""
            for j in chest_true_each:

                chest_true_updated = chest_true_updated + j+","

            chest_State_updated = chest_State_updated[:-1]
            chest_Time_updated = chest_Time_updated[:-1]
            chest_true_updated = chest_true_updated[:-1]

            query = chest_true_updated + "_" + chest_State_updated + "_" + chest_Time_updated


        if action == "add" :
            chest_type  = data
            for i in range(len(chest_details.split('_')[0].split(','))):
                if chest_details.split('_')[0].split(',')[i] == "0":
                    chest_details.split('_')[0].split(',')[i] == chest_type
                    break


        sql = "UPDATE PLAYER_INFO SET chest_info = %s WHERE id = %s"
        values = (query,player_id)
        self.cursor.execute(sql, values)
        self.db.commit()


    def update_player_res(self,data,player_id):
        coins_earned , diamonds_earned = data
        sql = "UPDATE PLAYER_INFO SET gold = %s , diamond = %s  WHERE id = %s"
        self.cursor.execute("SELECT gold,diamond FROM PLAYER_INFO WHERE id = \"" + player_id + "\"")
        player_details = []
        for i in self.cursor:
            player_details = list(i)

        player_details[0] = str(int(player_details[0]) + int(coins_earned))
        player_details[1] = str(int(player_details[1]) + int(diamonds_earned))
        player_details.append(player_id)

        values = tuple(player_details)

        self.cursor.execute(sql, values)
        self.db.commit()





