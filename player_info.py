import mysql.connector
import datetime

class player_info():
    def __init__(self,host,user,passwd,database):
        self.db = mysql.connector.connect(host=host,
                                     user=user,
                                     passwd=passwd,
                                     database=database)


        self.cursor = self.db.cursor()

    def get_user_details(self,user_id):
        a = []
        self.cursor.execute('SELECT user_name , games_won , games_lost , games_drawn , level , exp , badges , monsters_slayed ,gold,diamond , chest_info , emotes_owned , state FROM PLAYER_INFO WHERE id = \'' + user_id + '\'')
        for i in self.cursor:
            a.append(i)
        data = list(a[0])
        time_ = datetime.datetime.now()
        datetime_info = str(time_.month) + "/" +str(time_.day) + "/" + str(time_.year) + " "+ str(time_.hour)+":"+str(time_.minute)+":"+str(time_.second)
        data.append(datetime_info)
        return  data