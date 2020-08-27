import mysql.connector


class shop_manager:
    def __init__(self,host,user,passwd,database):
        self.db = mysql.connector.connect(host=host,
                                     user=user,
                                     passwd=passwd,
                                     database=database)


        self.cursor = self.db.cursor()
        self.emote_cost = ['300c','350c','400c','700c','1300c','250d','990c','680c']

    def buy_emote(self,item_n,player_id):
        self.cursor.execute("SELECT emotes_owned FROM PLAYER_INFO WHERE id = \"" + player_id + "\"")
        sql = "UPDATE PLAYER_INFO SET emotes_owned = %s  WHERE id = %s"
        emote_details_ = ""
        for i in self.cursor:
            emote_details_ = i[0]
        emote_each = emote_details_.split(',')
        emote_each[int(item_n)] = '1'
        values = ''
        for i in emote_each:
            values = values + i + ','

        values = (values[:-1],player_id)
        self.cursor.execute(sql, values)
        self.db.commit()




    def get_emote_cost(self):
        a = ''
        for i in self.emote_cost:
            a = a + i + ','
        return a[:-1]
