import compiler

program = [
    # insert the commands below this line
    ["LDA", "I0x02"],
    ["STA", "0x0000"],
    ["LDA", "I0x01"],
    ["LOOP"],
    ["PRA"],
    ["ADC", "0x0000"],
    ["BVS", "0xffff"],
    ["PHA"],
    ["LDA", "I0x01"],
    ["ADC", "0x0000"],
    ["STA", "0x0000"],
    ["PLA"],
    ["JMP", "LOOP"]
    # insert the commands above this line
]

compiler.compil(program)
