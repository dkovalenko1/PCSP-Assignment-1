import pandas as pd
from pathlib import Path
import matplotlib.pyplot as plt

csv_path = Path(__file__).resolve().parents[2] / 'bin' / 'Release' / 'net10.0' / 'benchmark_results.csv'
df = pd.read_csv(csv_path)

sizes_to_plot = [50, 100, 500, 1000, 10000]
threads_to_plot = [1, 6, 12, 24, 48, 12*8, 12*16]

baseline = df[df['Threads'] == 1].set_index('Size')['TotalTime_ms'].to_dict()

filtered_df = df[df['Threads'].isin(threads_to_plot)]

fig, (ax1, ax2) = plt.subplots(1, 2, figsize=(14, 6))

colors = plt.cm.tab10.colors[:len(sizes_to_plot)]

x_positions = range(len(threads_to_plot))
x_labels = [str(t) for t in threads_to_plot]

for i, size in enumerate(sizes_to_plot):
    data = filtered_df[filtered_df['Size'] == size].sort_values('Threads')
    ax1.plot(x_positions, data['TotalTime_ms'], 'o-',
             label=f'{size}x{size}', color=colors[i], linewidth=2, markersize=5)

ax1.set_xlabel('Number of Threads', fontsize=12)
ax1.set_ylabel('Execution Time (ms)', fontsize=12)
ax1.set_title('Effect of Thread Count on Performance', fontsize=14)
ax1.legend(title='Matrix Size', loc='upper right')
ax1.grid(True, alpha=0.3)
ax1.set_xticks(x_positions)
ax1.set_xticklabels(x_labels)

for i, size in enumerate(sizes_to_plot):
    data = filtered_df[filtered_df['Size'] == size].sort_values('Threads')
    speedup = baseline[size] / data['TotalTime_ms']
    ax2.plot(x_positions, speedup, 'o-',
             label=f'{size}x{size}', color=colors[i], linewidth=2, markersize=5)

ax2.set_xlabel('Number of Threads', fontsize=12)
ax2.set_ylabel('Speedup (x times faster)', fontsize=12)
ax2.set_title('Speedup vs Thread Count', fontsize=14)
ax2.legend(title='Matrix Size', loc='upper left')
ax2.grid(True, alpha=0.3)
ax2.set_xticks(x_positions)
ax2.set_xticklabels(x_labels)

plt.tight_layout()
plt.savefig('graphs1.png', dpi=150, bbox_inches='tight')
plt.show()

print("Saved to graphs1.png")
