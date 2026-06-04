export class SeatMap {
    constructor(containerId, onSeatClick) {
        this.container = document.getElementById(containerId);
        this.onSeatClick = onSeatClick;
        this.selectedSeats = [];
        this.reservedMode = false;
        this.grid = [];
    }

    render(sectors) {
        this.sectors = sectors;
        this.container.innerHTML = "";
        const { width, height, grid } = this.calculateGrid(sectors);
        
        this.setupGridContainer(width, height);
        this.renderGridCells(grid);
        this.toggleReservedMode(this.reservedMode);
    }

calculateGrid(sectors) {

    let maxY = 0;

    // límites reales horizontales
    const minX = Math.min(...sectors.map(s => s.x));
    const maxX = Math.max(...sectors.map(s => s.x + s.cols));

    // ancho REAL ocupado por sectores
    const width = maxX - minX;

    sectors.forEach(s => {
        const rows = Math.ceil(s.seats.length / s.cols);
        maxY = Math.max(maxY, s.y + rows);
    });

    const height = maxY +5;

    const grid = Array(width * height).fill(null);

    sectors.forEach(sector => {

        sector.seats.forEach((seat, i) => {

            // normalizar respecto al mínimo X
            const x = (sector.x - minX) + (i % sector.cols);

            const y = sector.y + Math.floor(i / sector.cols);

            grid[y * width + x] = seat;
        });
    });

    return { width, height, grid };
}

    setupGridContainer(width, height) {
        this.container.style.gridTemplateColumns = `repeat(${width}, 32px)`;
        this.container.style.gridTemplateRows = `repeat(${height}, 32px)`;
    }

    renderGridCells(grid) {
        this.grid = grid;
        grid.forEach((cell, index) => {
            const div = document.createElement("div");
            div.className = "cell";

            if (!cell) {
                this.container.appendChild(div);
                return;
            }

            const seatElement = this.createSeatElement(cell);
            div.appendChild(seatElement);
            this.container.appendChild(div);
        });
    }

    createSeatElement(seat) {
        const inner = document.createElement("div");
        let cls = "seat ";

        if (seat.status === 0) cls += "available";
        else if (seat.status === 1) cls += "reserved";
        else if (seat.status === 2) cls += "occupied";

        if (this.selectedSeats.includes(seat.id)) cls = "seat selected";

        inner.className = cls;
        inner.innerText = seat.rowIdentifier + seat.seatNumber;
        inner.onclick = () => this.onSeatClick(seat); 
        return inner;
    }

    selectSeat(seatId) {
        const index = this.selectedSeats.indexOf(seatId);
        if (index === -1) {
            this.selectedSeats.push(seatId);
        } else {
            this.selectedSeats.splice(index, 1);
        }
        this.render(this.sectors);
    }

    toggleReservedMode(enabled) {
        this.reservedMode = enabled;
        if (enabled) {
            this.container.classList.add("disabled-ui");
        } else {
            this.container.classList.remove("disabled-ui");
        }
    }

    clearSelection() {
        this.selectedSeats = [];
        this.render(this.sectors);
    }

    getSelectedSeats() {
        return [...this.selectedSeats];
    }

}