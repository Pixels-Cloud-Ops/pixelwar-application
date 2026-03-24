import { useEffect, useState } from "react";
import "./Grid.css";
import { getPixels, savePixel } from "./services/pixelService";

const COLS = 40;
const ROWS = 20;

const PALETTE = ["#ff0000", "#00ff00", "#0000ff", "#ffff00", "#000000", "#ffffff"];

function Grid() {
  const [pixelColors, setPixelColors] = useState<Record<string, string>>({});
  const [selectedColor, setSelectedColor] = useState<string>(PALETTE[0]);

  useEffect(() => {
    const fetchPixels = async () => {
      try {
        const pixels = await getPixels();

        const pixelsMap: Record<string, string> = {};
        for (const pixel of pixels) {
          const key = `${pixel.x},${pixel.y}`;
          pixelsMap[key] = pixel.color;
        }

        setPixelColors(pixelsMap);
        console.log("Pixels from backend:", pixels);
      } catch (error) {
        console.error("Error fetching pixels:", error);
      }
    };

    fetchPixels();
  }, []);

  const changeColor = async (x: number, y: number) => {
    const key = `${x},${y}`;

    try {
      await savePixel({
        x,
        y,
        color: selectedColor,
      });

      setPixelColors((prev) => ({
        ...prev,
        [key]: selectedColor,
      }));

      console.log(`color of pixel : {${x},${y}} has changed`);
    } catch (error) {
      console.error("Error saving pixel:", error);
    }
  };

  return (
    <div>
      <div className="title">Pixel War</div>

      <div className="palette">
        {PALETTE.map((c) => (
          <button
            key={c}
            className={`swatch ${selectedColor === c ? "swatch--active" : ""}`}
            style={{ backgroundColor: c }}
            onClick={() => setSelectedColor(c)}
            title={c}
            aria-label={`Select color ${c}`}
          />
        ))}
      </div>

      <div className="grid">
        {Array.from({ length: COLS * ROWS }).map((_, i) => {
          const x = i % COLS;
          const y = Math.floor(i / COLS);
          const key = `${x},${y}`;
          const color = pixelColors[key] ?? "#ffffff";

          return (
            <div
              key={key}
              className="pixel"
              style={{ backgroundColor: color }}
              onClick={() => changeColor(x, y)}
              title={`(${x},${y})`}
            />
          );
        })}
      </div>
    </div>
  );
}

export default Grid;