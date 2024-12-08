import os
import glob
import random
import shutil
import time

def cls():
    os.system('cls' if os.name=='nt' else 'clear')

# Now, to clear the screen
cls()

print("Hello world!\n")

#os.system("copy face.png face2.png")

letters="ABCDEDFGHIJKLMNOPQRSTUVWXYZ"
colors="green","pink","blue","yellow","orange","red","purple","black","white"

newpath='files'

reset=True
number=10
sleepTime=10
scrambleEverywhere=False

tries=0

file=open("difficultySettings.txt","r")
i=0
lines=file.readlines()


difficultyLevels=[]


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
        i=i+1
        tries+=1
    if n!=None and s!=None and se!=None:
        difficultyLevels.append([n,s,se])

file.close()

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
while(tries<1000):
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
        print("Everything is in its right place!")
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


# add difficulty: only in main folder vs everywhere
# add difficulty: speed