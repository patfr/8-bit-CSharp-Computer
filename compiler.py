def compil(program):
    index = 0
    labels = []
    finalProgram = []
    line = 0
    prefix = "0x"

    for x in program:
        if len(x[0]) > 3:
            labels.append([])
            labels[len(labels) - 1].append(x[0])
            labels[len(labels) - 1].append(line)
        if x[0].lower() == "adc":
            finalProgram.append(f"rom[{makeHex(line, 4)}] = 0x6d; // ADC {makeHex(int(x[1], 16), 4)}")
            finalProgram.append(f"rom[{makeHex(line + 1, 4)}] = {prefix + makeHex(int(x[1], 16), 4)[4:]};")
            finalProgram.append(f"rom[{makeHex(line + 2, 4)}] = {prefix + makeHex(int(x[1], 16), 4)[2:4]};")
            line += 3
        elif x[0].lower() == "bvs":
            finalProgram.append(f"rom[{makeHex(line, 4)}] = 0x70; // BVS {makeHex(int(x[1], 16), 4)}")
            finalProgram.append(f"rom[{makeHex(line + 1, 4)}] = {prefix + makeHex(int(x[1], 16), 4)[4:]};")
            finalProgram.append(f"rom[{makeHex(line + 2, 4)}] = {prefix + makeHex(int(x[1], 16), 4)[2:4]};")
            line += 3
        elif x[0].lower() == "clc":
            finalProgram.append(f"rom[{makeHex(line, 4)}] = 0x18; // CLC")
            line += 1
        elif x[0].lower() == "ext":
            finalProgram.append(f"rom[{makeHex(line, 4)}] = 0x03; // EXT")
            line += 1
        elif x[0].lower() == "inc":
            finalProgram.append(f"rom[{makeHex(line, 4)}] = 0xee; // INC {makeHex(int(x[1], 16), 4)}")
            finalProgram.append(f"rom[{makeHex(line + 1, 4)}] = {prefix + makeHex(int(x[1], 16), 4)[4:]};")
            finalProgram.append(f"rom[{makeHex(line + 2, 4)}] = {prefix + makeHex(int(x[1], 16), 4)[2:4]};")
            line += 3
        elif x[0].lower() == "jmp":
            index = 0
            for i in labels:
                if i[0].lower() == x[1].lower():
                    finalProgram.append(f"rom[{makeHex(line, 4)}] = 0x6c; // JMP {makeHex(i[1] + 0x8000, 4)}")
                    finalProgram.append(f"rom[{makeHex(line + 1, 4)}] = {prefix + makeHex(i[1] + 0x8000, 4)[4:]};")
                    finalProgram.append(f"rom[{makeHex(line + 2, 4)}] = {prefix + makeHex(i[1] + 0x8000, 4)[2:4]};")
                    line += 3
                index += 1
        elif x[0].lower() == "lda":
            if x[1][0:1].lower() == "a":
                finalProgram.append(f"rom[{makeHex(line, 4)}] = 0xad; // LDA {makeHex(int(x[1][1:], 16), 4)}")
                finalProgram.append(f"rom[{makeHex(line + 1, 4)}] = {prefix + makeHex(int(x[1][1:], 16), 4)[4:]};")
                finalProgram.append(f"rom[{makeHex(line + 2, 4)}] = {prefix + makeHex(int(x[1][1:], 16), 4)[2:4]};")
                line += 3
            elif x[1][0:1].lower() == "i":
                finalProgram.append(f"rom[{makeHex(line, 4)}] = 0xa9; // LDA {makeHex(int(x[1][1:], 16), 2)}")
                finalProgram.append(f"rom[{makeHex(line + 1, 4)}] = {makeHex(int(x[1][1:], 16), 2)};")
                line += 2
        elif x[0].lower() == "nop":
            finalProgram.append(f"rom[{makeHex(line, 4)}] = 0xea; // NOP")
            line += 1
        elif x[0].lower() == "pha":
            finalProgram.append(f"rom[{makeHex(line, 4)}] = 0x48; // PHA")
            line += 1
        elif x[0].lower() == "pla":
            finalProgram.append(f"rom[{makeHex(line, 4)}] = 0x68; // PLA")
            line += 1
        elif x[0].lower() == "pra":
            finalProgram.append(f"rom[{makeHex(line, 4)}] = 0x13; // PRA")
            line += 1
        elif x[0].lower() == "sta":
            finalProgram.append(f"rom[{makeHex(line, 4)}] = 0x8d; // STA {makeHex(int(x[1], 16), 4)}")
            finalProgram.append(f"rom[{makeHex(line + 1, 4)}] = {prefix + makeHex(int(x[1], 16), 4)[4:]};")
            finalProgram.append(f"rom[{makeHex(line + 2, 4)}] = {prefix + makeHex(int(x[1], 16), 4)[2:4]};")
            line += 3
    
    Com = open("Compiled.txt", "w")
    Com = open("Compiled.txt", "a")

    for x in finalProgram:
        Com.write("            " + x + "\n")
        print(x)
    
    Com.close()

def makeHex(num, length):
    h = hex(num)[2:]
    difference = length - len(h)

    for x in range(difference):
        h = "0" + h
    
    return "0x" + h
