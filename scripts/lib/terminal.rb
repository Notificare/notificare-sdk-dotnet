class String
  def red
    colorize(31)
  end

  def green
    colorize(32)
  end

  def yellow
    colorize(33)
  end

  def blue
    colorize(34)
  end

  def pink
    colorize(35)
  end

  def cyan
    colorize(36)
  end

  private

  def colorize(color_code)
    "\e[#{color_code}m#{self}\e[0m"
  end
end
