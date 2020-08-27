from flask import Flask
from flask import request
from signup import signup
from login import login
from player_info import player_info
from player_stats_update import player_stats
from shop import shop_manager
from match_manager import match_control
from host import master_control
from creation import create_db
app = Flask(__name__)


db_host = 'db_hostt'
db_user = 'db_user'
db_psswrd = 'db_psswrd'
db = 'db_name'

@app.route('/', methods=['GET', 'POST'])
def accept_request():

    if request.method == 'POST':
        keys = []

        for i in request.form.keys():
            keys.append(i)

        # print(keys)

        if 'login' in keys:
            data = request.form['login']

            login_player = login(db_host,db_user,db_psswrd,db)
            response = login_player.player_login_details(tuple(data.split('&')),request.form['type_of_login'])
            return str(response)

        if 'signup' in keys:
            data = request.form['signup']
            details = data.split('&')
            signup_player = signup(db_host,db_user,db_psswrd,db)
            availablity_username , response_code = signup_player.sign_up(details[0],details[1],details[2],details[3],details[4])
            return str(availablity_username) + '&' + str(response_code)

        if 'check_username' in keys:
            data = request.form['check_username']
            signup_player = signup(db_host,db_user,db_psswrd,db)
            a = str(signup_player.check_unique_data(('ds',(data))))
            return a

        if 'player_info' in keys:
            data = request.form['player_info']
            player_info_ = player_info(db_host,db_user,db_psswrd,db)
            details = ''
            for i in  player_info_.get_user_details(data):
                details = details + '&' + str(i)

            details = details[1:]
            return details

        if 'check_mailid' in keys:
            data = request.form['check_mailid']
            signup_player = signup(db_host,db_user,db_psswrd,db)
            a = str(signup_player.check_unique_data(((data),'ds')))
            return a

        if 'chest_update' in keys:
            chest_n = request.form['chest_update']
            player_id = request.form['id']
            action = request.form['action']
            chest_update = player_stats(db_host,db_user,db_psswrd,db)
            chest_update.update_chest_info(chest_n,player_id,action)
            return "DONE"

        if 'player_res_gained' in keys:
            res_gained = tuple( request.form['player_res_gained'].split('&'))
            player_id = request.form['id']
            res_update = player_stats(db_host,db_user,db_psswrd,db)
            res_update.update_player_res(res_gained,player_id)
            return "DONE"

        if 'emote_buy' in keys:
            bill_details = request.form['emote_buy'].split('&')
            shop_manage = shop_manager(db_host,db_user,db_psswrd,db)
            shop_manage.buy_emote(bill_details[0],bill_details[1])
            return "BILL ACCEPTED"

        if 'emote_cost' in keys:
            shop_manage = shop_manager(db_host,db_user,db_psswrd,db)
            return shop_manage.get_emote_cost()

        if 'match_making' in keys:
            player_details = request.form['match_making'].split('&')
            mc = match_control(db_host,db_user,db_psswrd,db)
            mc.add_player_to_que(player_details[0],list(player_details[1:]))
            return "ADDED"

        if 'remove' in keys:
            player_details = request.form['remove']
            mc = match_control(db_host,db_user,db_psswrd,db)
            mc.remove_player(player_details)
            return "CANCELLED"

        if 'check_for_match' in keys:
            player_details = request.form['check_for_match']
            mc = match_control(db_host,db_user,db_psswrd,db)
            a = mc.check_for_players(player_details)
            return a

        if 'opp_id' in keys:
            details = request.form['opp_id'].split('&')
            mc = match_control(db_host,db_user,db_psswrd,db)
            a = mc.get_opp_id(details[0],details[1])
            return  a

        if 'update_player' in keys:
            details = request.form['update_player'].split('&')
            mc = match_control(db_host,db_user,db_psswrd,db)
            mc.update_player_details(details[:-1],details[-1])
            return "UPDATED"

        if 'delete_room' in keys:
            room = request.form['delete_room']
            mc = match_control(db_host,db_user,db_psswrd,db)
            mc.delete_the_room(room)
            return "DONE"



        if 'get_player_game_details' in keys:
            details = request.form['get_player_game_details'].split('&')
            mc = match_control(db_host,db_user,db_psswrd,db)
            a = mc.get_data_to_render(details[0],details[1])
            return a[0] + "&"  +a[1]

        if 'master_control' in keys:
            details_query = request.form['master_control'].split('&')
            data = ""
            if len(details_query)==1:
                a = master_control(db_host,db_user,db_psswrd,db)
                data = a.action_to_do("read",details_query[0])

            if len(details_query)==2:
                a = master_control(db_host,db_user,db_psswrd,db)
                data = a.action_to_do("modify",details_query[0],details_query[1])

            return data

        if 'create_db' in keys:
            a = create_db(db_host,db_user,db_psswrd,db)
            return "SUCESS"

        if 'testing' in keys:
            import requests
            import time

            a = time.time()

            requests.post('https://thekeres.herokuapp.com/', {"master_control": 'SELECT * FROM MATCH_MAKING'})

            b = time.time()

            c = b - a
            print(c)
            return str(c)


    else :
        return 'invalid'

