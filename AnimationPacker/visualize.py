import os
import io
import json
import pygame
import zipfile

# CODE FOR IMAGE VISUALISATION
def visualize(file_path):
    ext = os.path.basename(file_path).split(".")[-1]
    if ext != "anim":
        print("Not a valid file format")
        return
    
    with zipfile.ZipFile(file_path) as animation:
        image = animation.read("image.png")
        data = json.loads(animation.read("data.json"))
    
    bytes_io = io.BytesIO(image)
    img = pygame.image.load(bytes_io)

    rects = []
    for d in data["frames"]:
        rect = pygame.Rect(d["x"], d["y"], d["w"], d["h"])
        rects.append(rect)

    win = pygame.display.set_mode(img.get_size())
    pygame.display.set_caption(os.path.basename(file_path))

    run = True
    while run:
        for e in pygame.event.get():
            if e.type == pygame.KEYDOWN and e.key == pygame.K_ESCAPE:
                run = False
                break

        win.fill("black")

        win.blit(img, (0, 0))

        for r in rects:
            pygame.draw.rect(win, "red", r, 1)

        pygame.display.flip()
