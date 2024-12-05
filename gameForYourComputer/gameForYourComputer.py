import os
import glob
import random
import shutil
import time

print("hello world!")

#os.system("copy face.png face2.png")

letters="ABCDEDFGHIJKLMNOPQRSTUVWXYZ"
colors="green","pink","blue","yellow","orange","red","purple","black","white"

newpath='files'

reset=True
number=10

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
        print(colors[i])
        newpath=os.path.join('files',colors[i])
        if not os.path.exists(newpath):
            os.mkdir(newpath)

    for i in range(number):
        k=random.randint(1,len(colors))
        fileName=letters[random.randint(0,len(letters)-1)]+str(i)+str(k)+".png"
        os.system("copy face"+str(k)+".png "+fileName)
        os.replace(fileName,"files/"+fileName)




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
while(tries<100):
    time.sleep(10)
    tries+=1

    everythingRight=True

    fd=[]
    fn=[]
    dirs=[]
    for (dirpath, dirnames, filenames) in os.walk("files"):
        for n in filenames:
            fn.append(n)
            fd.append(dirpath+"/"+n)

            if everythingRight==True:
                num=int(n[-5])-1
                col=dirpath.replace("files","").replace("/","").replace("\\","")
                print(colors[num])
                print(col)
                if colors[num]!=col:
                    everythingRight=False

        dirs.append(dirpath)

    if everythingRight:
        print("Everything is right!")
        break

    i=random.randint(0,len(fd)-1)
    newpath=dirs[random.randint(0,len(dirs)-1)]+"/"+fn[i]
    os.replace(fd[i],newpath)

    print("Scrambled!")
    print(fd[i])
    print(newpath)


# add difficulty: only in main folder vs everywhere
# add difficulty: speed