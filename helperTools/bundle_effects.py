import sys
import os
import json

subdirectories = [
    "Vest",
    "Arm",
    "Head",
    "Hand",
    "Foot"
]

strip_keys = [
    "id",
    "createdAt",
    "description",
    "updatedAt"
]

def write(contents, path):
    file = open(path, "w")
    file.write(contents)
    file.close()

def preprocessEffect(key, effect):
    for key in strip_keys:
        if key in effect:
            del effect[key]
        
    effect["name"] = key
        
    return effect["project"]

def parseContents(filesContents, preprocess = lambda x, y : y):
    output = {}
    
    for key, content in filesContents.items():
        try:
            parsed = json.loads(content)
        except Exception:
            print("Error while parsing, ignoring")
            continue
        
        output[key] = preprocess(key, parsed)
        
    return output

def readFiles(files):
    output = {}
    
    for key, file in files.items():
        try:
            f = open(file, "r")
            output[key] = f.read()
            f.close()
        except Exception:
            print("failed to read %s" % file)
            continue
            
    return output

def findFiles(path):
    output = {}
    for file in os.listdir(path):
        if not file.endswith(".tact"):
            continue
        
        output[os.path.splitext(file)[0]] = os.path.abspath(os.path.join(path, file))
    
    return output

def run(effects_folder, output_file):
    keyedContents = {}
    
    for directory in subdirectories:
        path = os.path.join(effects_folder, directory)
        files = findFiles(path)
        loadedFiles = readFiles(files)
        keyedContents[directory] = parseContents(loadedFiles, preprocessEffect)

    output = json.dumps(keyedContents)
    write(output, output_file)

def validateEffects(path):
    if not os.path.isdir(path):
        print("Invalid effects folder")
        return False

    return True

def validateOutput(path):
    try:
        if not os.path.isdir(os.path.dirname(path)):
            print("invalid output")
            return False
    except Exception:
        print("invalid output")
        return False
    
    return True

def enforceOutput(path):
    if os.path.isdir(path):
        return os.path.join(path, "effects.json")
    return path

def main():
    if len(sys.argv) != 3:
        print("usage: bundle_effects.py <effects folder> <output bundle>")
        exit(1)
        
    effects_folder = sys.argv[1]
    output_file = sys.argv[2]
    
    if not validateEffects(effects_folder) or not validateOutput(output_file):
        exit(1)
        
    effects_folder = os.path.abspath(effects_folder)
    output_file = enforceOutput(os.path.abspath(output_file))
    
    run(effects_folder, output_file)

if __name__ == '__main__':
    main()
