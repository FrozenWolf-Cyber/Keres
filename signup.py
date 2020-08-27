import mysql.connector
import random
import string

class signup:
    def __init__(self,host,user,passwd,database):
        self.db = mysql.connector.connect(host=host,
                                     user=user,
                                     passwd=passwd,
                                     database=database)


        self.cursor = self.db.cursor()

    def generate_unique_id(self):
        unique_id = ''
        for i in range(random.randrange(8,15)):
            unique_id = unique_id + random.choice(string.printable[:61])

        return unique_id

    def check_unique_data(self,data):
        check = True
        self.cursor.execute("SELECT mail_id , user_name FROM PLAYER_LOGIN")
        for i in self.cursor:
            #mail_id , user_nam
            if data[0] == i[0] or data[1] == i[1]:
                check = False
                break

        return check

    def update_db(self,data):
        user_name , password , mail_id ,name ,age , user_id = data
        self.cursor.execute('INSERT INTO PLAYER_LOGIN (mail_id , user_name , password , id) VALUES (%s, %s, %s, %s)',(mail_id,user_name,password,user_id))
        self.cursor.execute('INSERT INTO PLAYER_INFO (id , name , DOB , games_won , games_lost , games_drawn , level , exp , badges , user_name , monsters_slayed , gold , diamond , chest_info , emotes_owned) VALUES (%s, %s, %s, %s, %s,%s, %s, %s, %s, %s , %s , %s , %s , %s , %s)',(user_id,name,age,0,0,0,1,0,'none',user_name,0,0,0,'1,2,0,0,0_n,n,n,n,n_n,n,0,0,0','0,0,0,0,0,0,0,0'))
        self.db.commit()
        self.cursor.close()
        self.db.close()
        return 1

    def sign_up(self,mail_id,user_name,password,name,age):
        user_name_availablity = self.check_unique_data((mail_id,user_name))
        database_update_code = 0
        if user_name_availablity:
            unique_id = self.generate_unique_id()
            database_update_code = self.update_db((user_name,password,mail_id,name,age,unique_id))

        return  user_name_availablity , database_update_code












