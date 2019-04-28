from flask import Flask, session, url_for, redirect, request, render_template, abort, flash, Markup, json
from datetime import datetime
from flask_sqlalchemy import SQLAlchemy
import random
import os
from  sqlalchemy.sql.expression import func

app = Flask(__name__)
app.secret_key = "debug-attivo"
app.config['SQLALCHEMY_DATABASE_URI'] = 'sqlite:///db.sqlite'
app.config['SQLALCHEMY_TRACK_MODIFICATIONS'] = False
db = SQLAlchemy(app)
app.config.from_object(__name__)


# Database Classes go beyond this line

class Message(db.Model):
    mid = db.Column(db.Integer, primary_key=True)
    floor = db.Column(db.Integer, nullable=False)
    content = db.Column(db.String, nullable=False)

    def toJSON(self):
        return {"Message": {'floor': self.floor,
                            'content': self.content}}

    def __init__(self, content, floor):
        self.content = content
        self.floor = floor

    def __repr__(self):
        return self.content


def give_json_response(floor):
    messages = Message.query.filter_by(floor=floor).order_by(func.random()).limit(5).all()
    return json.dumps([message.toJSON() for message in messages])


# Website pages and API functions go beyond this line
@app.route("/message/<int:floor>", methods=["GET", "POST"])
def page_message(floor):
    if request.method == "GET":
        return give_json_response(floor)
    newmessage = Message(request.form['content'], floor)
    db.session.add(newmessage)
    db.session.commit()
    return "Success."


if __name__ == "__main__":
    # Se non esiste il database viene creato
    if not os.path.isfile("db.sqlite"):
        newmessage = Message("Welcome to the dungeon, mortal.", 1)
        db.create_all()
        db.session.add(newmessage)
        db.session.commit()
    app.run(debug=True)
