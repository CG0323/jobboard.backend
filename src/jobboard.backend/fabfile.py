#coding=utf-8
from __future__ import with_statement
from fabric.api import local, env, settings, abort, run, cd, lcd
from fabric.contrib.console import confirm
import time

# env.use_ssh_config = True
env.hosts = ['hostip:port']
env.user = 'root'
env.key_filename = 'C:\Users\mac\Documents\id_rsa_mopyfish'

def publish():
    local("iisreset /stop")
    local("dotnet publish -o c:\Users\mac\jobboard.backend")
    local("iisreset /start")
def push():
	with lcd('C:\Users\mac\jobboard.backend'):
		local("git add .")
		with settings(warn_only=True):
			local("git commit -m 'auto_update'")
		local("git push")

def update_server():
	with cd("/home/cg/jobboard.backend"):
		run('git pull') 
		run('supervisorctl restart jobboard.backend')
	
def deploy():
	publish()
	push()
	update_server()

def migrate(name):
	local("dotnet ef migrations add %s" % name)
	local("dotnet ef database update")

def commit():
	with lcd('C:\Users\mac\jobboard.source'):
		local("git add .")
		local("git commit")
		local("git push")
	
