require 'fileutils'
require_relative 'system'
require_relative 'terminal'

class CSProject
  attr_reader :component, :platform, :directory

  def initialize(component:, platform:, directory:)
    @component = component
    @platform = platform
    @directory = directory
  end

  class << self
    def all
      [
        CSProject.new(
          component: :core,
          platform: :android,
          directory: 'Notificare.Android.Binding'
        ),
        CSProject.new(
          component: :core,
          platform: :ios,
          directory: 'Notificare.iOS.Binding'
        ),
        CSProject.new(
          component: :assets,
          platform: :android,
          directory: 'Notificare.Assets.Android.Binding'
        ),
        CSProject.new(
          component: :assets,
          platform: :ios,
          directory: 'Notificare.Assets.iOS.Binding'
        ),
        CSProject.new(
          component: :geo,
          platform: :android,
          directory: 'Notificare.Geo.Android.Binding'
        ),
        CSProject.new(
          component: :geo,
          platform: :ios,
          directory: 'Notificare.Geo.iOS.Binding'
        ),
        CSProject.new(
          component: :in_app_messaging,
          platform: :android,
          directory: 'Notificare.InAppMessaging.Android.Binding'
        ),
        CSProject.new(
          component: :in_app_messaging,
          platform: :ios,
          directory: 'Notificare.InAppMessaging.iOS.Binding'
        ),
        CSProject.new(
          component: :inbox,
          platform: :android,
          directory: 'Notificare.Inbox.Android.Binding'
        ),
        CSProject.new(
          component: :inbox,
          platform: :ios,
          directory: 'Notificare.Inbox.iOS.Binding'
        ),
        CSProject.new(
          component: :loyalty,
          platform: :android,
          directory: 'Notificare.Loyalty.Android.Binding'
        ),
        CSProject.new(
          component: :loyalty,
          platform: :ios,
          directory: 'Notificare.Loyalty.iOS.Binding'
        ),
        CSProject.new(
          component: :notification_service_extension,
          platform: :ios,
          directory: 'Notificare.NotificationServiceExtension.iOS.Binding'
        ),
        CSProject.new(
          component: :push,
          platform: :android,
          directory: 'Notificare.Push.Android.Binding'
        ),
        CSProject.new(
          component: :push,
          platform: :ios,
          directory: 'Notificare.Push.iOS.Binding'
        ),
        CSProject.new(
          component: :push_ui,
          platform: :android,
          directory: 'Notificare.Push.UI.Android.Binding'
        ),
        CSProject.new(
          component: :push_ui,
          platform: :ios,
          directory: 'Notificare.Push.UI.iOS.Binding'
        ),
        CSProject.new(
          component: :scannables,
          platform: :android,
          directory: 'Notificare.Scannables.Android.Binding'
        ),
        CSProject.new(
          component: :scannables,
          platform: :ios,
          directory: 'Notificare.Scannables.iOS.Binding'
        ),
        CSProject.new(
          component: :user_inbox,
          platform: :android,
          directory: 'Notificare.UserInbox.Android.Binding'
        ),
        CSProject.new(
          component: :user_inbox,
          platform: :ios,
          directory: 'Notificare.UserInbox.iOS.Binding'
        ),
      ]
    end

    def find(component, platform)
      all.find { |e| e.component == component && e.platform == platform }
    end
  end

  def clean
    Dir.chdir(directory) do
      execute_system_command('dotnet clean')
    end
  end

  def build
    Dir.chdir(directory) do
      execute_system_command('dotnet build')
    end
  end

  def generate_binding_sources
    raise 'Cannot generate sharpie sources for non-iOS projects.' if platform != :ios

    puts "▸ Running Objective-Sharpie".blue
    sharpie_out = run_objective_sharpie

    copy_generated_api_definitions(sharpie_out)
    copy_generated_structs_and_enums(sharpie_out)
  end

  def xcframeworks
    file_contents = File.read(File.join(directory, "#{directory}.csproj"))
    file_contents.scan(/libs\\|\/(\w+.xcframework)/).flatten
  end

  protected def root_namespace
    file_contents = File.read(File.join(directory, "#{directory}.csproj"))
    file_contents.match(/<RootNamespace>(.+)<\/RootNamespace>/).captures[0]
  end

  private def binding_scheme
    file_contents = File.read(File.join(directory, "#{directory}.csproj"))
    file_contents.match(/<SchemeName>(.+)<\/SchemeName>/).captures[0]
  end

  private def run_objective_sharpie
    command = <<~COMMAND
      sharpie bind --output=sharpie-out \
        --namespace=#{root_namespace} \
        --sdk=#{latest_sharpie_sdk} \
        --scope=Headers \
        Headers/#{binding_scheme}-Swift.h
    COMMAND

    framework = File.join(directory, 'bin', 'Debug', 'net9.0-ios', "#{directory}.resources", "#{binding_scheme}iOS.xcframework", 'ios-arm64', "#{binding_scheme}.framework")
    Dir.chdir(framework) do
      puts "▸ Running Objective-Sharpie".blue
      puts command
      execute_system_command(command)
    end

    "#{framework}/sharpie-out"
  end

  private def latest_sharpie_sdk
    _, output = capture_system_command_output('sharpie xcode -sdks', stream_output: false)
    output.match(/(iphoneos\d+\.\d+)/).captures[0]
  end

  private def depends_on_core
    core_project = CSProject.find(:core, platform)

    contents = File.read(File.join(directory, "#{directory}.csproj"))
    contents.include?(core_project.directory)
  end

  private def copy_generated_api_definitions(sharpie_out)
    filename = File.join(sharpie_out, 'ApiDefinitions.cs')
    return unless File.file?(filename)

    contents = File.read(filename)

    puts "▸ Removing scheme's default namespace".blue
    contents = contents.gsub(/using #{binding_scheme};(\n)?/, '')

    if depends_on_core
      puts "▸ Including core's namespace in imports".blue

      contents = <<~CONTENT
      using #{CSProject.find(:core, platform).root_namespace};

      #{contents}
      CONTENT
    end

    puts "▸ Applying known exceptions to generated code".blue

    # iOS NotificareAsset needs to conform to INativeObject because 'fetch' returns an
    # NSArray<NotificareAsset> and NSArray<T> requires T to be INativeObject.
    if component == :assets && platform == :ios
      contents = contents.gsub(/interface NotificareAsset$/, 'interface NotificareAsset : INativeObject')
    end

    File.write(File.join(directory, 'ApiDefinitions.cs'), contents)
  end

  private def copy_generated_structs_and_enums(sharpie_out)
    filename = File.join(sharpie_out, 'StructsAndEnums.cs')
    return unless File.file?(filename)

    FileUtils.cp(filename, directory)
  end
end
