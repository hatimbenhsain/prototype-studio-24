filepath=__file__

import os
import random
import shutil
import time
import os
from win32com.client import Dispatch
import subprocess
from mutagen.mp3 import MP3 

tries=0

file=open("settings.txt","r")
i=0
lines=file.readlines()

difficultyLevels=[]

createFiles=True
playMusic=True
playSortingGame=True

while i<len(lines) and tries<1000:
    n=None
    s=None
    se=None
    while i<len(lines) and (lines[i].strip()=="" or n==None or s==None or se==None) and tries<1000:
        line=lines[i]
        if line[0:7]=="number:":
            n=int(line.replace("number:","").strip())
        elif line[0:10]=="sleepTime:":
            s=int(line.replace("sleepTime:","").strip())
        elif line[0:19]=="scrambleEverywhere:":
            se=bool(int(line.replace("scrambleEverywhere:","").strip()))
        elif line[0:12]=="createFiles:":
            createFiles=bool(int(line.replace("createFiles:","").strip()))
        elif line[0:10]=="playMusic:":
            playMusic=bool(int(line.replace("playMusic:","").strip()))
        elif line[0:16]=="playSortingGame:":
            playSortingGame=bool(int(line.replace("playSortingGame:","").strip()))
        i=i+1
        tries+=1
    if n!=None and s!=None and se!=None:
        difficultyLevels.append([n,s,se])

file.close()

if createFiles: 

    newpath="C:/users/hatim/"
    popcorntext="\t=== popcorn sorting algorithm ===\n\n1. Acquire n ideal corns and n uniform cooking environment. Assign to each corn an array value.\n2. Initialize an empty array B of size equal to array A.\n3. Cook each corn an amount of time equivalent to its corresponding array value, then immediately remove it from heat and seal it in a hermetic container.\n4. Cook all of the corns at once on separate identical pans.\n5. When a corn pops, turn off the heat for all the corns and add its corresponding value to the end of array B. Dispose of this corn.\n6. Go back to step 4 until you have run out of corn.\n7. Array B contains our sorted array.\n\n\t=== additional notes ===\n\n1. For accuracy, ensure all the corn is cooked in identical conditions (same temperature, barometric pressure, surface contact, etc.)\n2. If some values are non-positive, add a constant k bigger than the absolute of the smallest value in the array to all the values.\n3. To ensure that none of the corn pops during step 3, divide all of the values by a constant n such that they all fall below the minimum time to pop a corn in our cooking environment.\n4. While this algorithm runs in O(n) time, it also requires O(n) humans (or machines) to operate in a perfectly accurate way such that all of the corn is cooked for the same amount of time."

    if os.path.exists("C:/users"):
        if not os.path.exists(newpath):
            os.mkdir(newpath)
        newpath=newpath+"Documents/"
        if not os.path.exists(newpath):
            os.mkdir(newpath)

        newpath2=newpath+"Ideas/"
        if not os.path.exists(newpath2):
            os.mkdir(newpath2)
        newpath2=newpath2+"popcornsort.txt"
        file=open(newpath2,"w")
        file.write(popcorntext)
        file.close()

        newpath=newpath+"My Games/"
        if not os.path.exists(newpath):
            os.mkdir(newpath)
        
        path=r"C:/users/hatim/Documents/My Games/Game For Your Computer.lnk"
        target=os.path.dirname(filepath)
        work_dir=os.path.dirname(filepath)

        shell = Dispatch('WScript.Shell')
        shortcut = shell.CreateShortCut(path)
        shortcut.Targetpath = target
        shortcut.WorkingDirectory = work_dir
        shortcut.save()

def cls():
    os.system('cls' if os.name=='nt' else 'clear')

musicFiles=[]
musicDuration=0
musicIndex=0
musicTimer=0
lastTime=time.time()

def music():
    global musicIndex, musicDuration, musicTimer
    path="music\\"+musicFiles[musicIndex]
    if os.path.exists(newpath):
        os.system(path)
        audio=MP3(path) 
        musicDuration=audio.info.length
    musicIndex=musicIndex+1
    musicIndex=musicIndex%len(musicFiles)
    musicTimer=0

if playSortingGame:
    newpath='sortingGame\sortingGame.exe'
    if os.path.exists(newpath):
        process = subprocess.Popen(newpath, stdout=subprocess.PIPE, creationflags=0x08000000)
        process = subprocess.Popen(newpath, stdout=subprocess.PIPE, creationflags=0x08000000)
        process = subprocess.Popen(newpath, stdout=subprocess.PIPE, creationflags=0x08000000)
        process = subprocess.Popen(newpath, stdout=subprocess.PIPE, creationflags=0x08000000)
        process = subprocess.Popen(newpath, stdout=subprocess.PIPE, creationflags=0x08000000)
        process = subprocess.Popen(newpath, stdout=subprocess.PIPE, creationflags=0x08000000)
        process = subprocess.Popen(newpath, stdout=subprocess.PIPE, creationflags=0x08000000)
        process = subprocess.Popen(newpath, stdout=subprocess.PIPE, creationflags=0x08000000)
        process = subprocess.Popen(newpath, stdout=subprocess.PIPE, creationflags=0x08000000)
        process = subprocess.Popen(newpath, stdout=subprocess.PIPE, creationflags=0x08000000)

if playMusic:
    newpath="music"
    for (dirpath, dirnames, filenames) in os.walk(newpath):
        for n in filenames:
            musicFiles.append(n)
    if len(musicFiles)>0:
        musicIndex=random.randint(0,len(musicFiles)-1)
        musicDuration=1
        music()
        lastTime=time.time()

print("Hello world!\n")

#os.system("copy face.png face2.png")

letters="ABCDEDFGHIJKLMNOPQRSTUVWXYZ"
colors="green","pink","blue","yellow","orange","red","purple","black","white"

newpath='files'

reset=True
number=10
sleepTime=10
scrambleEverywhere=False


def prepareGame():

    cls()

    tries=0
    global sleepTime, scrambleEverywhere,number

    while(tries<1000):
        tries+=1
        difficultyLevel=int(input("Please choose a difficulty level from 1 to "+str(len(difficultyLevels))+", or "+str(len(difficultyLevels)+1)+" to choose your own settings.\n"))
        
        if difficultyLevel==len(difficultyLevels)+1:
            number=int(input("Choose the number of files to organize.\n"))
            sleepTime=int(input("Choose how many seconds the computer takes before scrambling files again.\n"))
            scrambleEverywhere=bool(int(input("Choose whether the computer should scramble files everywhere (1) or only in the main file folder (0).\n")))
            break
        elif difficultyLevel>0 and difficultyLevel<len(difficultyLevels)+1:
            number=difficultyLevels[difficultyLevel-1][0]
            sleepTime=difficultyLevels[difficultyLevel-1][1]
            scrambleEverywhere=bool(difficultyLevels[difficultyLevel-1][2])
            break
        else:
            print("Invalid input.")
            continue

    print("\nPreparing the game...\n")

    newpath="files"

    if reset:

        if os.path.exists(newpath):
            shutil.rmtree(newpath)

        if not os.path.exists(newpath):
            os.mkdir(newpath)

        # alphabetic version
        # for i in range(len(letters)):
        #     print(letters[i])
        #     newpath=os.path.join('files',letters[i])
        #     if not os.path.exists(newpath):
        #         os.mkdir(newpath)


        # for i in range(len(letters)):
        #     for n in range(random.randint(1,12)):
        #         k=random.randint(1,4)
        #         file=letters[i]+str(n)+".png"
        #         os.system("copy face"+str(k)+".png "+file)
        #         os.replace(file,"files/"+file)

        #color version
        for i in range(len(colors)):
            newpath=os.path.join('files',colors[i])
            if not os.path.exists(newpath):
                os.mkdir(newpath)

        for i in range(number):
            k=random.randint(1,len(colors))
            fileName=letters[random.randint(0,len(letters)-1)]+str(i)+str(k)+".png"
            os.system("copy face"+str(k)+".png "+fileName)
            os.replace(fileName,"files/"+fileName)

    print("\nFinished preparing.\n")
    print("Your challenge is to sort all the files in /files while the computer is simultaneously scrambling them!\n")

prepareGame()

# scramble
# fd=[]
# fn=[]
# dirs=[]
# for (dirpath, dirnames, filenames) in os.walk("files"):
#     for n in filenames:
#         fn.append(n)
#         fd.append(dirpath+"/"+n)
#     dirs.append(dirpath)

# print(fd)
# print(fn)
# print(dirs)

# for i in range(len(fn)):
#     os.replace(fd[i],dirs[random.randint(0,len(dirs)-1)]+"/"+fn[i])


tries=0
while(True):
    if playMusic:
        musicTimer=musicTimer+(time.time()-lastTime)
        lastTime=time.time()
        if musicTimer>musicDuration:
            music()

    time.sleep(sleepTime)
    tries+=1

    everythingRight=True

    fd=[]
    fn=[]
    dirs=[]
    for (dirpath, dirnames, filenames) in os.walk("files"):
        for n in filenames:
            n=n.replace(" - Copy","")
            fn.append(n)
            fd.append(dirpath+"/"+n)

            if everythingRight==True:
                num=int(n[-5])-1
                col=dirpath.replace("files","").replace("/","").replace("\\","")
                if colors[num]!=col:
                    everythingRight=False

        dirs.append(dirpath)

    if everythingRight:
        print("\nEverything is in its right place!\n")
        playAgain=bool(int(input("Will you play again? (1 for yes, 0 for no.)\n")))
        if playAgain:
            prepareGame()
            continue
        else:
            print("\nGoodbye!")
            break

    i=random.randint(0,len(fd)-1)
    if(scrambleEverywhere):
        newpath=dirs[random.randint(0,len(dirs)-1)]+"/"+fn[i]
    else:
        newpath="files/"+fn[i]
    os.replace(fd[i],newpath)

    print("\nScrambled!")
    print(fd[i])
    print(newpath.replace("\\","/"))