import os
import sys
import json
import zipfile
import argparse
import pygame
import visualize


# ARGUMENT PARSER
parser = argparse.ArgumentParser(
    prog="",
    description="Animation packer by @Red_Parrot. Packs animation sprite sheet into special archive format, that contains original image and frames data",
    epilog="End"
)

parser.add_argument(
    "filepath", 
    help="Path to animation sprite sheet"
)

parser.add_argument(
    "-c", "--count",
    type=int,
    help="Frames count in sprite sheet",
    default=1
)

parser.add_argument(
    "-o", "--orient",
    dest="orient",
    choices=["h", "v"], 
    default="h",
    help="Sprite sheet orientation: [h]orizontal or [v]ertical"
)

parser.add_argument(
    "-n", "--name",
    dest="name",
    help="Specify output file name. Default is image name"
)

parser.add_argument(
    "-v", "--visual",
    dest="visual",
    action="store_true",
    help="Passing this argument with path to packed animation will visalize frames"
)

args = parser.parse_args()

# ACTUAL PROGRAM

pygame.init()

def end():
    pygame.quit()
    exit()

file_path = args.filepath
count = args.count
orient = args.orient

if (args.visual):
    # Go to visualization
    visualize.visualize(file_path)
    end()

if not os.path.exists(file_path):
    print("No image file by path: {0}".format(file_path))
    end()

try:
    img = pygame.image.load(file_path)
except:
    print("Invalid image")
    end()

tokens = os.path.basename(file_path).split(".")
file_name = tokens[0]
ext = tokens[-1].lower()

if ext != "png":
    print("PNG files support only")
    end()

name = args.name if args.name is not None else file_name

img_w = img.get_width()
img_h = img.get_height()

cell_w = img_w // count if orient == "h" else img_w
cell_h = img_h // count if orient == "V" else img_h

data = {
    "imageWidth": img_w,
    "imageHeight": img_h,
    "cellWidth": cell_w,
    "cellHeight": cell_h,
    "framesCount": count,
    "frames": []
}

# CALCULATION LOOP
row = 0
col = 0
for i in range(count):
    if orient == "h":
        col = i
    else:
        row = i

    x = col * cell_w
    y = row * cell_h

    frame_rect = pygame.Rect(x, y, cell_w, cell_h)
    sub_frame = img.subsurface(frame_rect)
    sub_frame_bounds = sub_frame.get_bounding_rect()
    sub_frame_bounds.x += x
    sub_frame_bounds.y += y

    frame_data = {
        "x": sub_frame_bounds.x,
        "y": sub_frame_bounds.y,
        "w": sub_frame_bounds.w,
        "h": sub_frame_bounds.h,
        "bounds": [
            sub_frame_bounds.x,
            sub_frame_bounds.y,
            sub_frame_bounds.w,
            sub_frame_bounds.h
        ]
    }

    data["frames"].append(frame_data)

json_data = json.dumps(data, ensure_ascii=False, indent=2)
# PACKING
animation = zipfile.ZipFile("{0}.anim".format(name), "w", compresslevel=zipfile.ZIP_STORED)
animation.write(file_path, "image.png")
animation.writestr("data.json", json_data)
